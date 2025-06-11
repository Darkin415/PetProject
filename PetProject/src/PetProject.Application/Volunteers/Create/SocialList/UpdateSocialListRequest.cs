namespace PetProject.Application.Volunteers.Create.SocialList;

public record UpdateSocialListRequest(IEnumerable<SocialListDto> SocialMedias);

public record SocialListDto(string Title, string LinkMedia);