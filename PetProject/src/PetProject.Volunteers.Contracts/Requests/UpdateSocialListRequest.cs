using PetProject.Core.DTOs;
using PetProject.Volunteers.Contracts.DTOs;

namespace PetProject.Volunteers.Contracts.Requests;

public record UpdateSocialListRequest(IEnumerable<SocialListDto> SocialMedias);



