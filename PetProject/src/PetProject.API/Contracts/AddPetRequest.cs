using PetProject.Application.Volunteers.Create.Pet;
using PetProject.Domain;

namespace PetProject.API.Contracts;

public record AddPetRequest(
    string NickName,
    string View,
    double Weight,
    double Height,
    string Color,
    AttributeDto Attribute,
    string Breed,
    string StatusHealth,
    string OwnerTelephonNumber,
    string CastrationStatus,
    string VaccinationStatus,
    DateTime BirthDate,
    StatusHelp Status,
    DateTime DateOfCreation,
    IFormFileCollection Photos);

