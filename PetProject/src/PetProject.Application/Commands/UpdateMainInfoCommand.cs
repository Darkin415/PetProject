using PetProject.Contracts.Dtos;

namespace PetProject.Application.Commands;

public record UpdateMainInfoCommand(
    Guid VolunteerId,
    FullNameDto FullName,
    string TelephonNumber,
    string Description);
