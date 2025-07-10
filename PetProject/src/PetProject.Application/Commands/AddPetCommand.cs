using PetProject.Contracts.Dtos;
using PetProject.Domain.Enum;
namespace PetProject.Contracts.Commands
{
    public record AddPetCommand(
    Guid VolunteerId,
    string NickName,
    string Breed,
    string Species,
    AttributeDto PhysicalAttribute,
    string Color,
    string StatusHealth,
    string OwnerTelephonNumber,
    string CastrationStatus,
    string VaccinationStatus,
    DateTime BirthDate,
    StatusHelp Status,
    DateTime DateOfCreation) : ICommand;
}