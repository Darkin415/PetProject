using CSharpFunctionalExtensions;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PetProject.Domain.Volunteers;
public class Pet : Shared.Entity<PetId>
{
    private Pet(PetId id) : base(id)
    {
    }
    private Pet(
        PetId id,
        NickName nickname,
        View view,
        Breed breed,
        Color color,
        StatusHealth statusHealth,
        PhysicalAttributes attributes,
        TelephonNumber telephoneNumber,
        CastrationStatus castrationStatus,
        DateTime birthDate,
        VaccinationStatus vaccinationStatus,
        StatusHelp status,
        DateTime dateOfCreation) : base(id)
    {
        Nickname = nickname;
        View = view;
        Breed = breed;
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

    public View View { get; private set; } = default!;

    public Breed Breed { get; private set; } = default!;

    public Color Color { get; private set; } = default!;

    public StatusHealth StatusHealth { get; private set; } = default!;

    public PhysicalAttributes Attributes { get; private set; }

    public TelephonNumber OwnerTelephoneNumber { get; private set; } = default!;

    public CastrationStatus CastrationStatus { get; private set; } = default!;

    public DateTime BirthDate { get; private set; }

    public VaccinationStatus VaccinationStatus { get; private set; } = default!;

    public StatusHelp Status { get; private set; }

    public DateTime DateOfCreation { get; private set; }

    public static Result<Pet, string> Create(
        NickName nickname,
        View view,
        Breed breed,
        Color color,
        StatusHealth statusHealth,
        PhysicalAttributes attributes,
        TelephonNumber telephonNumber,
        CastrationStatus castrationStatus,
        DateTime birthDate,
        VaccinationStatus vaccinationStatus,
        StatusHelp status,
        DateTime dateOfCreation)
    {

        if (birthDate == DateTime.MinValue)
        
            return "Birthdate can not be null";
        

        if (dateOfCreation == DateTime.MinValue)
        
            return "Date of creation can not be null";
        
        else
        {
            var pet = new Pet(
                PetId.NewGuidId(),
                 nickname,
                 view,
                 breed,
                 color,
                 statusHealth,
                 attributes,
                 telephonNumber,
                 castrationStatus,
                 birthDate,
                 vaccinationStatus,
                 status,
                 dateOfCreation);

            return pet;
        }
    }
}

