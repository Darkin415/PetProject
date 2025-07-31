using CSharpFunctionalExtensions;
using PetProject.Contracts;
using PetProject.Contracts.Ids;
using PetProject.Contracts.ValueObjects;
using PetProject.Volunteers.Domain.Enum;

namespace PetProject.Volunteers.Domain.Pets;
public class Pet : Contracts.Entity<PetId>, ISoftDeletable
{
    private bool _isDeleted = false;
    private readonly List<Photos> _photos = [];

    private Pet(PetId id) : base(id)
    {
    }
    public Pet(
        PetId id,
        NickName nickname,
        PetInfo petInfo,
        Color? color,
        StatusHealth statusHealth,
        Weight? weight,
        Height? height,
        TelephonNumber telephoneNumber,
        CastrationStatus castrationStatus,
        BirthDay birthDate,
        VaccinationStatus vaccinationStatus,
        StatusHelp status,
        DateOfCreation dateOfCreation
        ) : base(id)
    {
        Nickname = nickname;
        PetInfo = petInfo;
        Color = color;
        StatusHealth = statusHealth;
        Weight = weight;
        Height = height;
        OwnerTelephoneNumber = telephoneNumber;
        CastrationStatus = castrationStatus;
        BirthDate = birthDate;
        VaccinationStatus = vaccinationStatus;
        Status = status;
        DateOfCreation = dateOfCreation;
    }
    public NickName Nickname { get; private set; } = default!;
    
    public Weight Weight { get; private set; }
    
    public Height Height { get; private set; }
    

    public IReadOnlyList<Photos> Photos => _photos;

    public Position Position { get; private set; }
    
    public PetInfo PetInfo { get; private set; }

    public Color Color { get; private set; } = default!;

    public StatusHealth StatusHealth { get; private set; } = default!;
    
    public TelephonNumber OwnerTelephoneNumber { get; private set; } = default!;

    public CastrationStatus CastrationStatus { get; private set; } = default!;

    public BirthDay BirthDate { get; private set; }

    public VaccinationStatus VaccinationStatus { get; private set; } = default!;

    public StatusHelp Status { get; private set; }

    public DateOfCreation DateOfCreation { get; private set; }

    

    public void Delete()
    {
        _isDeleted = true;

    }

    public void Restore()
    {
        _isDeleted = false;
    }

    public UnitResult<Error> RemovePhotos(IEnumerable<Photos> photos)
    {
        foreach (var photo in photos)
        {
            _photos.Remove(photo);
        }

        return Result.Success<Error>();
    }

    public UnitResult<Error> AddPhotos(IEnumerable<Photos> photos)
    {
        _photos.AddRange(photos.ToList());

        return UnitResult.Success<Error>();
    }

    public void SetSerialNumber(Position serialNumber) =>
        Position = serialNumber;

    public UnitResult<Error> MoveForward()
    {
        var newPosition = Position.Forward();

        if (newPosition.IsFailure)
            return newPosition.Error;

        Position = newPosition.Value;

        return Result.Success<Error>();
    }

    public UnitResult<Error> MoveBack()
    {
        var newPosition = Position.Back();

        if (newPosition.IsFailure)
            return newPosition.Error;

        Position = newPosition.Value;

        return Result.Success<Error>();
    }

    public void Move(Position newPosition) =>    
        Position = newPosition;
    
}