using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetProject.Application.Abstraction;
using PetProject.Application.Commands;
using PetProject.Application.Database;
using PetProject.Application.Providers;
using PetProject.Contracts.Commands;
using PetProject.Domain;
using PetProject.Domain.PetSpecies;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObjects;




namespace PetProject.Application.Volunteers.Create.Pet.AddPet;


public class AddPetHandler : ICommandHandler<Guid, AddPetCommand> 
{
    private const string BUCKET_NAME = "photos";   
    private readonly ILogger<AddPetHandler> _logger;
    private readonly IFilesProvider _fileProvider;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddPetCommand> _validator;
    public AddPetHandler(
        IFilesProvider fileProvider,
        IValidator<AddPetCommand> validator,
        ISpeciesRepository speciesRepository,
        IVolunteersRepository volunteersRepository,
        ILogger<AddPetHandler> logger,
        IUnitOfWork unitOfWork
        )
    {
        _logger = logger;
        _validator = validator;
        _fileProvider = fileProvider;
        _volunteersRepository = volunteersRepository;
        _speciesRepository = speciesRepository;
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

            var speciesId = SpeciesId.NewSpeciesId();

            var breedId = BreedId.NewBreedId();

            var species = Species.Create(speciesId);
            
            var breed = Breed.Create(breedId);

            // var species = await _speciesRepository.GetSpeciesAsync(speciesId, cancellationToken);
            // if (species.IsFailure)
            //     return species.Error.ToErrorList();
            //
            // var breed = await _speciesRepository.GetBreedAsync(breedId, cancellationToken);
            // if (breed.IsFailure)
            //     return breed.Error.ToErrorList();

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

            var pet = new Domain.Volunteers.Pet(
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
