namespace PetProject.API.Module;
public record CreateVolunteerRequest(
    string Title,
    string LinkMedia,
    string Information,
    string Email,
    string PhoneNumber,
    FullNameDto FullName,
    IEnumerable<SocialMediaDto> SocialMedias
);

public record SocialMediaDto(string Title, string LinkMedia);

public record FullNameDto(string FirstName, string LastName, string? Surname);
