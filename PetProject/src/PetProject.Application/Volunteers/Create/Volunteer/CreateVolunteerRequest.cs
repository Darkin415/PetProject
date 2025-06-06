namespace PetProject.Application.Volunteers.Create.Volunteer;
public record CreateVolunteerRequest(
    string Title,
    string LinkMedia,
    string Information,
    string Email,
    string PhoneNumber,
    CreateFullNameDto FullName,
    IEnumerable<SocialMediaDto> SocialMedias
);

public record SocialMediaDto(string Title, string LinkMedia);

public record CreateFullNameDto(string FirstName, string LastName, string? Surname);
