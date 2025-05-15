namespace PetProject.API.Module;
public record CreateVolunteerRequest(
    string Link,
    string Information,
    string Email,
    string PhoneNumber,
    string FirstName,
    string LastName,
    string? Surname,
    IEnumerable<SocialMediaDto> SocialMedias
);
public record SocialMediaDto(string Title, string LinkMedia);
