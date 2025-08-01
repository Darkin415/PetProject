using PetProject.Volunteers.Domain.Enum;

namespace PetProject.Volunteers.Controllers.Pets.Requests;

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
   
}

