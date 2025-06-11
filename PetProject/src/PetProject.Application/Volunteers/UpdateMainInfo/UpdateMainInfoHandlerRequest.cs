using PetProject.Domain.Shared.ValueObject;
using PetProject.Domain;

namespace PetProject.Application.Volunteers.UpdateMainInfo;
public record UpdateMainInfoRequest(FullNameDto FullName, string TelephonNumber, string Description);

public record FullNameDto(string FirstName, string LastName, string? Surname);

