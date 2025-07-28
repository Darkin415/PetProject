using PetProject.Domain.Enum;

namespace PetProject.API.Requests;

public record AddPetRequest(
    string NickName,
    string Species,
    string Breed,
    string Color,
    double Weight,
    double Height,
    string StatusHealth,
    string OwnerTelephonNumber,
    string CastrationStatus,
    string VaccinationStatus,
    DateTime BirthDate,
    StatusHelp Status,
    DateTime DateOfCreation)
{
    // public AddPetCommand ToCommand(Guid id) => new(
    //     id,
    //     NickName,
    //     Breed,
    //     Species,
    //     Attribute,
    //     Color,
    //     StatusHealth,
    //     OwnerTelephonNumber,
    //     CastrationStatus,
    //     VaccinationStatus,
    //     BirthDate, 
    //     Status, 
    //     DateOfCreation);
}

