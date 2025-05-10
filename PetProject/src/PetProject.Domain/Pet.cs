using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PetProject.Domain;
public class Pet : Shared.Entity<PetId>
{
    private Pet(PetId id) : base(id)
    {
    }
    private Pet(
        PetId id,
        string nickname,
        string view,
        string breed,
        string color,
        string statusHealth,
        double weight,
        double height,
        string telephoneNumber,
        string castrationStatus,
        DateTime birthDate,
        string vaccinationStatus,
        StatusHelp status,
        DateTime dateOfCreation) : base(id)
    {
        Nickname = nickname;
        View = view;
        Breed = breed;
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
    public string Nickname { get; private set; } = default!;
    public string View { get; private set; } = default!;
    public string Breed { get; private set; } = default!;
    public string Color { get; private set; } = default!;
    public string StatusHealth { get; private set; } = default!;
    public double Weight { get; private set; }
    public double Height { get; private set; }
    public string OwnerTelephoneNumber { get; private set; } = default!;
    public string CastrationStatus { get; private set; } = default!;
    public DateTime BirthDate { get; private set; }
    public string VaccinationStatus { get; private set; } = default!;
    public StatusHelp Status { get; private set; }
    public DateTime DateOfCreation { get; private set; }
    public static Result<Pet, string> Create(
        string nickname,
        string view,
        string breed,
        string color,
        string statusHealth,
        double weight,
        double height,
        string telephonNumber,
        string castrationStatus,
        DateTime birthDate,
        string vaccinationStatus,
        StatusHelp status,
        DateTime dateOfCreation)
    {
        if (string.IsNullOrWhiteSpace(nickname))
        {
            return "Nickname can not be empty";
        }
        if (string.IsNullOrWhiteSpace(view))
        {
            return "View can not be empty";
        }
        if (string.IsNullOrWhiteSpace(breed))
        {
            return "Breed can not be empty";
        }
        if (string.IsNullOrWhiteSpace(color))
        {
            return "Color can not be empty";
        }
        if (string.IsNullOrWhiteSpace(statusHealth))
        {
            return "StatusHealth can not be empty";
        }
        if (weight == 0)
        {
            return "Weight can not be empty";
        }
        if (height == 0)
        {
            return "Height can not be empty";
        }
        if (string.IsNullOrWhiteSpace(telephonNumber))
        {
            return "Telephon number can not be empty";
        }
        if (string.IsNullOrWhiteSpace(castrationStatus))
        {
            return "Castration status can not be empty";
        }
        if (birthDate == DateTime.MinValue)
        {
            return "Birthdate can not be null";
        }
        if (string.IsNullOrWhiteSpace(vaccinationStatus))
        {
            return "Vaccination  status can not be empty";
        }
        if (dateOfCreation == DateTime.MinValue)
        {
            return "Date of creation can not be null";
        }
        else
        {
            var pet = new Pet(
                PetId.NewGuidId(),
                 nickname,
                 view,
                 breed,
                 color,
                 statusHealth,
                 weight,
                 height,
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

