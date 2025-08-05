using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetProject.Core.Abstraction;
using PetProject.Core.Database;
using PetProject.Core.ValueObject;
using PetProject.Files.Contracts;
using PetProject.SharedKernel;
using PetProject.SharedKernel.ValueObjects;
using PetProject.Species.Contracts;
using PetProject.Species.Domain.PetSpecies;
using PetProject.Volunteers.Contracts.ids;
using PetProject.Volunteers.Domain.Entities;
using PetProject.Volunteers.Domain.PetValueObjects;

namespace PetProejct.Volunteers.Application.Commands.CreatePet;


public class AddPetHandler : ICommandHandler<Guid, AddPetCommand> 
{
    private const string BUCKET_NAME = "photos";   
    private readonly ILogger<AddPetHandler> _logger;
    private readonly IFilesContract _fileContract;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ISpeciesContract _speciesContract;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddPetCommand> _validator;
    public AddPetHandler(
        IFilesContract fileContract,
        IValidator<AddPetCommand> validator,
        ISpeciesContract speciesContract,
        IVolunteersRepository volunteersRepository,
        ILogger<AddPetHandler> logger,
        IUnitOfWork unitOfWork
        )
    {
        _logger = logger;
        _validator = validator;
        _fileContract = fileContract;
        _volunteersRepository = volunteersRepository;
        _speciesContract = speciesContract;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid, ErrorList>> Handle(
    AddPetCommand command,
    CancellationToken cancellationToken = default)
    {       
        try
        {
            var volunteerResult = await _volunteersRepository
                .GetVolunteerById(VolunteerId.Create(command.VolunteerId).Value, cancellationToken);

            if (volunteerResult.IsFailure)
                return volunteerResult.Error.ToErrorList();

            var petId = PetId.NewPetId();
            

            var nickName = NickName.Create(command.NickName).Value;
            

            var titleSpecies = Title.Create(command.Species);
            if (titleSpecies.IsFailure)
                return titleSpecies.Error.ToErrorList();

            var titleBreed = Title.Create(command.Breed);
            if (titleSpecies.IsFailure)
                return titleSpecies.Error.ToErrorList();
            

            var species = await _speciesContract.GetSpeciesByNameAsync(
                titleSpecies.Value.Name, cancellationToken);
            if (species.IsFailure)
                return species.Error.ToErrorList();

            var speciesId = SpeciesId.Create(species.Value.Id).Value;
            
            var breed = await _speciesContract.GetBreedByNameAsync(
                titleBreed.Value.Name, cancellationToken);
            if (breed.IsFailure)
                return breed.Error.ToErrorList();
            
            var breedId = BreedId.Create(breed.Value.Id).Value;

            var petInfo = PetInfo.Create(speciesId, breedId).Value;

            var statusHealth = StatusHealth.Create(command.StatusHealth).Value;

            var ownerTelephonNumber = TelephonNumber.Create(command.OwnerTelephonNumber).Value;

            var castrationStatus = CastrationStatus.Create(command.CastrationStatus).Value;

            var vaccinationStatus = VaccinationStatus.Create(command.VaccinationStatus).Value;

            var birthDate = BirthDay.Create(command.BirthDate).Value;

            var dateOfCreation = DateOfCreation.Create(command.DateOfCreation).Value;

            var color = Color.Create(command.Color).Value;

            var weight = Weight.Create(command.Weight).Value;

            var height = Height.Create(command.Height).Value;

            
            var status = command.StatusHelp;

            var pet = new Pet (
            petId,
            nickName,
            petInfo,
            color,
            statusHealth,
            weight,
            height,
            ownerTelephonNumber,
            castrationStatus,
            birthDate,
            vaccinationStatus,
            status,
            dateOfCreation);

            volunteerResult.Value.AddPet(pet);

            await _unitOfWork.SaveChanges(cancellationToken);   

            return pet.Id.Value;
        }

        catch (Exception ex)
        {

            _logger.LogError(ex, "Can not add pet to volunteer - {id} in transaction", command.VolunteerId);

            return Error.Failure("AddPetFailed", ex.Message).ToErrorList();
        }
    }
}
