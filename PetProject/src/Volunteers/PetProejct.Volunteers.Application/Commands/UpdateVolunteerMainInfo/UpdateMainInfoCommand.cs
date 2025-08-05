using PetProject.Core.DTOs;
using PetProject.Files.Contracts.Dtos;
using PetProject.Volunteers.Contracts.DTOs;

namespace PetProejct.Volunteers.Application.Commands.UpdateVolunteerMainInfo;

public record UpdateMainInfoCommand(
    Guid VolunteerId,
    FullNameDto FullName,
    string TelephonNumber,
    string Description);
