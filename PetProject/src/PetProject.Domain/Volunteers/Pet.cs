using CSharpFunctionalExtensions;
using PetProject.Domain.Enum;
using PetProject.Domain.PetSpecies;
using PetProject.Domain.Shared;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObject;
namespace PetProject.Domain.Volunteers;
public class Pet : Shared.Entity<PetId>, ISoftDeletable
{
    private bool _isDeleted = false;
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
        DateOfCreation dateOfCreation,
        IReadOnlyList<Photos> photosList) : base(id)
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
        Photos = photosList;
    }
    public NickName Nickname { get; private set; } = default!;
   
    public IReadOnlyList<Photos> Photos { get; private set; } = new List<Photos>();

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
}

