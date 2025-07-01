using CSharpFunctionalExtensions;
using PetProject.Domain.Enum;
using PetProject.Domain.PetSpecies;
using PetProject.Domain.Shared;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObject;
using System.Collections.Generic;
namespace PetProject.Domain.Volunteers;
public class Pet : Shared.Entity<PetId>, ISoftDeletable
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
        PhysicalAttributes? attributes,
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
        Attributes = attributes;
        OwnerTelephoneNumber = telephoneNumber;
        CastrationStatus = castrationStatus;
        BirthDate = birthDate;
        VaccinationStatus = vaccinationStatus;
        Status = status;
        DateOfCreation = dateOfCreation;
    }
    public NickName Nickname { get; private set; } = default!;

    public IReadOnlyList<Photos> Photos => _photos;

    public Position Position { get; private set; }


    public PetInfo PetInfo { get; private set; }

    public Color Color { get; private set; } = default!;

    public StatusHealth StatusHealth { get; private set; } = default!;

    public PhysicalAttributes Attributes { get; private set; }

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

