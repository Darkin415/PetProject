using PetProject.Contracts.Dtos;

namespace PetProejct.Volunteers.Application.Commands;

public record UpdateMainInfoCommand(
    Guid VolunteerId,
    FullNameDto FullName,
    string TelephonNumber,
    string Description);
