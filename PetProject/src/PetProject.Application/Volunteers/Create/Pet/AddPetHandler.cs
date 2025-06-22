using CSharpFunctionalExtensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using PetProject.Application.Database;
using PetProject.Application.FileProvider;
using PetProject.Application.Providers;
using PetProject.Application.Volunteers.Create.SocialList;
using PetProject.Domain;
using PetProject.Domain.PetSpecies;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObject;
using PetProject.Domain.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PetProject.Application.Volunteers.Create.Pet;


public class AddPetHandler
{
    private const string BUCKET_NAME = "photos";
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddPetHandler> _logger;
    private readonly IFilesProvider _fileProvider;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ISpeciesRepository _speciesRepository;
    public AddPetHandler(
        IFilesProvider fileProvider,
        ISpeciesRepository speciesRepository,
        IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        ILogger<AddPetHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _fileProvider = fileProvider;
        _volunteersRepository = volunteersRepository;
        _speciesRepository = speciesRepository;
    }
    public async Task<Result<Guid, Error>> Handle(
    AddPetCommand command,
    CancellationToken cancellationToken = default)
    {
        var transaction = await _unitOfWork.BeginTransaction(cancellationToken);

        try
        {
            var volunteerResult = await _volunteersRepository
                .GetById(VolunteerId.Create(command.VolunteerId).Value, cancellationToken);

            if (volunteerResult.IsFailure)
                return volunteerResult.Error;

            var petId = PetId.NewPetId();

            var nickName = NickName.Create(command.NickName).Value;

            var speciesId = SpeciesId.NewSpeciesId();

            var breedId = BreedId.NewBreedId();

            var species = await _speciesRepository.GetSpeciesAsync(speciesId, cancellationToken);

            var breed = await _speciesRepository.GetBreedAsync(breedId, cancellationToken);

            var petInfo = PetInfo.Create(speciesId, breedId).Value;

            var statusHealth = StatusHealth.Create(command.StatusHealth).Value;

            var ownerTelephonNumber = TelephonNumber.Create(command.OwnerTelephonNumber).Value;

            var castrationStatus = CastrationStatus.Create(command.CastrationStatus).Value;

            var vaccinationStatus = VaccinationStatus.Create(command.VaccinationStatus).Value;

            var birthDate = BirthDay.Create(command.BirthDate).Value;

            var dateOfCreation = DateOfCreation.Create(command.DateOfCreation).Value;

            var color = Color.Create(command.Color).Value;

            var attribute = PhysicalAttributes.Create(command.PhysicalAttribute.Weight, command.PhysicalAttribute.Height).Value;

            var status = command.Status;

            List<FileData> filesData = [];
            

            foreach (var file in command.Photos)
            {
                var extensions = Path.GetExtension(file.FileName);

                var filePath = FilePath.Create(Guid.NewGuid(), extensions);
                if (filePath.IsFailure)
                    return filePath.Error;

                var fileContent = new FileData(file.Content, filePath.Value, BUCKET_NAME);

                filesData.Add(fileContent);             
            }

            var photosFile = filesData
                .Select(f => f.FilePath)
                .Select(f => new Photos(f))
                .ToList();

            var pet = new PetProject.Domain.Volunteers.Pet(
            petId,
            nickName,
            petInfo,
            color,
            statusHealth,
            attribute,
            ownerTelephonNumber,
            castrationStatus,
            birthDate,
            vaccinationStatus,
            status,
            dateOfCreation,
            photosFile
            );

            volunteerResult.Value.AddPet(pet);

            await _unitOfWork.SaveChanges(cancellationToken);

            var uploadResult = await _fileProvider.UploadFiles(filesData, cancellationToken);

            if (uploadResult.IsFailure)
                return uploadResult.Error;

            transaction.Commit();

            return pet.Id.Value;
        }

        catch (Exception ex)
        {

            _logger.LogError(ex, "Can not add pet to volunteer - {id} in transaction", command.VolunteerId);

            transaction.Rollback();

            return Error.Failure("volunteer.pet.failure", "Can not add pet to volunteer - {id}");
        }                                                  
    }
}
