using PetProject.Domain.Shared.ValueObject;

namespace PetProject.Application.Volunteers.UpdateMainInfo;
public record UpdateMainInfoRequest(Guid VolunteerId, FullNameDto FullName, string TelephonNumber, string Description);

public record FullNameDto(string FirstName, string LastName, string? Surname);