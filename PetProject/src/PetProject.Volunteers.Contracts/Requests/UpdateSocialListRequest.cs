using PetProject.Core.DTOs;
using PetProject.Files.Contracts.Dtos;
using PetProject.Volunteers.Contracts.DTOs;

namespace PetProject.Volunteers.Contracts.Requests;

public record UpdateSocialListRequest(IEnumerable<SocialListDto> SocialMedias);



