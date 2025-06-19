using CSharpFunctionalExtensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using PetProject.Application.FileProvider;
using PetProject.Application.Providers;
using PetProject.Application.Volunteers.Create.SocialList;
using PetProject.Domain;
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
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<AddPetHandler> _logger;
    private readonly IFilesProvider _fileProvider;
    public AddPetHandler(
        IFilesProvider fileProvider,
        IVolunteersRepository volunteersRepository,
        ILogger<AddPetHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _fileProvider = fileProvider;
    }
    public async Task<Result<string, Error>> Handle(
    AddPetCommand command,
    CancellationToken cancellationToken = default)
    {
        var volunteerResult = await _volunteersRepository
            .GetById(VolunteerId.Create(command.VolunteerId).Value, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        var petId = PetId.NewPetId();

        var nickName = NickName.Create(command.NickName).Value;

        var view = View.Create(command.View).Value;

        var breed = Breed.Create(command.Breed).Value;

        var statusHealth = StatusHealth.Create(command.StatusHealth).Value;

        var ownerTelephonNumber = TelephonNumber.Create(command.OwnerTelephonNumber).Value;

        var castrationStatus = CastrationStatus.Create(command.CastrationStatus).Value;

        var vaccinationStatus = VaccinationStatus.Create(command.VaccinationStatus).Value;

        var birthDate = BirthDay.Create(command.BirthDate).Value;

        var dateOfCreation = DateOfCreation.Create(command.DateOfCreation).Value;

        var color = Color.Create(command.Color).Value;

        var attribute = PhysicalAttributes.Create(command.PhysicalAttribute.Weight, command.PhysicalAttribute.Height).Value;

        var status = command.Status;

        foreach (var file in command.Photos)
        {
            var extensions = Path.GetExtension(file.FileName);

            var filePath = Guid.NewGuid() + "." + extensions;

            var fileData = new FileData(file.Content, BUCKET_NAME, filePath);
        }

        var photosResult = command.Photos
    .Select(f => Photos.Create(f.FileName)).ToList();

        var photosList = photosResult.Select(f => f.Value).ToList();


        var pet = new PetProject.Domain.Volunteers.Pet(
            petId,
            nickName,
            view,
            breed,
            color,
            statusHealth,
            attribute,
            ownerTelephonNumber,
            castrationStatus,
            birthDate,
            vaccinationStatus,
            status,
            dateOfCreation,
            photosList
            );

        volunteerResult.Value.AddPet(pet);

        await _volunteersRepository.Save(volunteerResult.Value, cancellationToken);

        return pet.Id.Value.ToString();
;
    }
}
