using PetProject.Core.DTOs;
using PetProject.Volunteers.Contracts.DTOs;

namespace PetProject.Volunteers.Contracts.Requests;

public record UpdateMainInfoRequest(FullNameDto FullName, string TelephonNumber, string Description);

