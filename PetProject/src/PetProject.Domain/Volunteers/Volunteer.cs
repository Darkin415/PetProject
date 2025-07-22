using CSharpFunctionalExtensions;
using PetProject.Domain.Enum;
using PetProject.Domain.Shared;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObjects;
namespace PetProject.Domain.Volunteers;
public class Volunteer : Shared.Entity<VolunteerId>, ISoftDeletable
{
    private bool _isDeleted = false;
    private readonly List<Pet> _pets = [];


    private Volunteer(VolunteerId id) : base(id)
    {

    }
    public Volunteer(
    VolunteerId id,
    FullName fullName,
    Email email,
    Description description,
    TelephonNumber telephoneNumber,
    IReadOnlyList<SocialMedia>? socialMedias = null

) : base(id)
    {
        FullName = fullName;
        Email = email;
        Description = description;
        TelephonNumber = telephoneNumber;
        Socials = socialMedias ?? new List<SocialMedia>();
    }

    public FullName FullName { get; private set; }
    public IReadOnlyList<SocialMedia> Socials { get; private set; } = new List<SocialMedia>();
    public Email Email { get; private set; } = default!;
    public Description Description { get; private set; } = default!;
    public IReadOnlyList<Pet> Pets => _pets;
    public TelephonNumber TelephonNumber { get; private set; }
    public int CountPetFoundHome(List<Pet> pets)
    {
        return Pets?.Count(pet => pet.Status == StatusHelp.FoundedHome) ?? 0;
    }
    public int LookingHouse(List<Pet> pets)
    {
        return Pets?.Count(pet => pet.Status == StatusHelp.SeekingHome) ?? 0;
    }
    public int CountBeUnderTreatment(List<Pet> pets)
    {
        return Pets?.Count(pet => pet.Status == StatusHelp.BeUnderTreatment) ?? 0;
    }
    public static Result<Volunteer, string> Create(
        Email email,
        Description description,
        FullName fullName,
        TelephonNumber telephonNumber,
        IReadOnlyList<SocialMedia>? socialMedias = null)
    {
        var volunteer = new Volunteer(
        VolunteerId.NewVolunteerId(),
        fullName,
        email,
        description,
        telephonNumber,
        socialMedias
    );

        return volunteer;
    }

    public void UpdateMainInfo(FullName fullName, Description description, TelephonNumber telephonNumber)
    {
        FullName = fullName;
        Description = description;
        TelephonNumber = telephonNumber;
    }

    public void UpdateSocialList(IReadOnlyList<SocialMedia> socialMedias)
    {
        Socials = socialMedias;
    }

    public void Delete()
    {
        _isDeleted = true;

    }

    public void Restore()
    {
        _isDeleted = false;
    }

    public UnitResult<Error> AddPet(Pet pet)
    {
        var serialNumberResult = Position.Create(_pets.Count + 1);
        if (serialNumberResult.IsFailure)
            return serialNumberResult.Error;

        pet.SetSerialNumber(serialNumberResult.Value);

        _pets.Add(pet);
        return Result.Success<Error>();
    }


    public Result<Pet, Error> GetPetById(PetId id)
    {
        var pet = _pets.FirstOrDefault(p => p.Id == id);

        if (pet == null)
            return Errors.General.NotFound(id.Value);

        return pet;
    }

    public UnitResult<Error> MovePet(Pet pet, Position newPosition)
    {
        var currentPosition = pet.Position;

        if (currentPosition == newPosition || _pets.Count == 1)
            return Result.Success<Error>();

        var adjustPosition = AdjustNewPositionIfOutOfRange(newPosition);

        if (adjustPosition.IsFailure)
            return adjustPosition.Error;

        newPosition = adjustPosition.Value;

        var moveResult = MovePetBetweenPosition(newPosition, currentPosition);
        if (moveResult.IsFailure)
            return moveResult.Error;

        pet.Move(newPosition);

        return Result.Success<Error>();

    }
    
    private UnitResult<Error> MovePetBetweenPosition(Position newPosition, Position currentPosition)
    {
        if (newPosition.Value < currentPosition.Value)
        {
            var petsToMove = _pets.Where(p => p.Position.Value >= newPosition.Value
            && p.Position.Value < currentPosition.Value);

            foreach (var petToMove in petsToMove)
            {
                var result = petToMove.MoveForward();
                if (result.IsFailure)
                    return result.Error;
            }
        }
      
        else if (newPosition.Value > currentPosition.Value)
        {
            var petsToMove = _pets.Where(p => p.Position.Value > currentPosition.Value
            && p.Position.Value <= newPosition.Value);

            foreach (var petToMove in petsToMove)
            {
                var result = petToMove.MoveBack();
                if (result.IsFailure)
                    return result.Error;
            }
        }
        return Result.Success<Error>();
    }

    private Result<Position, Error> AdjustNewPositionIfOutOfRange(Position newPosition)
    {
        if (newPosition.Value < _pets.Count)
            return newPosition;

        var lastPosition = Position.Create(_pets.Count);

        if (lastPosition.IsFailure)
            return lastPosition.Error;

        return lastPosition.Value;
    }
    
    public UnitResult<Error> RemovePet(PetId id)
    {
        var petToRemove = _pets.FirstOrDefault(p => p.Id == id);

        var removePosition = petToRemove.Position.Value;
        _pets.Remove(petToRemove);

        var petToMove = _pets.Where(p => p.Position.Value > removePosition);

        foreach(var pet in petToMove)
        {
            pet.MoveBack();
        }

        return Result.Success<Error>();
            
    } 
    
}
