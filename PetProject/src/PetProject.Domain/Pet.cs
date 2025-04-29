using PetProject.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PetProject.Domain;
public class Pet : Entity<PetId>
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

    // Свойства (все с private set)
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
}
// public class Pet : Entity<PetId>
// {
//     // ef core
//     private Pet(PetId id) :base(id)
//     {

//     }
//     public string Nickname { get; private set; } = default!;
//     public string View { get; private set; } = default!;
//     public string Breed { get; private set; } = default!;
//     public string Color { get; private  set; } = default!;
//     public StatusHelp Status { get; private set; }
//     public string StatusHealth { get; private set; } = default!;
//     public  double Weight { get; private set; }
//     public double Height { get; private  set; }
//     public string TelephoneNumber { get; private set; } = default!;
//     public string CastrationStatus { get; private set; } = default!;
//     public DateTime BirthDate { get; private set; }
//     public string VaccinationStatus { get; private set; } = default!; 
//     public DateTime DateOfCreation { get; private set; }
//     private Pet(
//         PetId Petid, 
//         string nicname,
//         string view, 
//         string breed, 
//         string color, 
//         string statusHealth, 
//         double weight, 
//         double height, 
//         string telephoneNumber, 
//         string castrationStatus, 
//         DateTime birthdate, 
//         string vaccinationStatus,
//         StatusHelp statusHelp,
//         DateTime dateOfCreation 
//         ) : base (Petid)
//     {
//         Nickname = nicname;
//         View = view;
//         Breed = breed;
//         Color = color;
//         StatusHealth = statusHealth;
//         Weight = weight;
//         Height = height;
//         TelephoneNumber = telephoneNumber;
//         CastrationStatus = castrationStatus;
//         BirthDate = birthdate;
//         VaccinationStatus = vaccinationStatus;
//         DateOfCreation = dateOfCreation;
//     } 
//}
