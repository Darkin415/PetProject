using PetProject.Application.Volunteers.Create.Pet.AddPet;
using PetProject.Contracts.Commands;
using PetProject.Contracts.Dtos;
using PetProject.Domain.Enum;

namespace PetProject.API.Contracts;

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

