namespace PetProject.API.Module;
public record CreateVolunteerRequest(
    string Title,
    string LinkMedia,
    string Information,
    string Email,
    string PhoneNumber,
    FullNameDtos FullName,
    IEnumerable<SocialMediaDto> SocialMedias
);

public record SocialMediaDto(string Title, string LinkMedia);

public record FullNameDtos(string FirstName, string LastName, string? Surname);
