using PetProject.Contracts.Abstraction;
using PetProject.Volunteers.Domain.Enum;

namespace PetProejct.Volunteers.Application.Commands
{
    public record AddPetCommand(
    Guid VolunteerId,
    string NickName,
    string Breed,
    string Species,
    double Weight,
    double Height,
    string Color,
    string StatusHealth,
    string OwnerTelephonNumber,
    string CastrationStatus,
    string VaccinationStatus,
    DateTime BirthDate,
    StatusHelp Status,
    DateTime DateOfCreation) : ICommand;
}