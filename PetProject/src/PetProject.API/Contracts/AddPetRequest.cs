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
    AttributeDto Attribute,    
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

