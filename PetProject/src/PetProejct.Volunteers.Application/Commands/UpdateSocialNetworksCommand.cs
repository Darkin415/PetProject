using PetProject.Contracts.Abstraction;
using PetProject.Contracts.Dtos;

namespace PetProejct.Volunteers.Application.Commands;

public record UpdateSocialNetworksCommand(Guid VolunteerId, IEnumerable<SocialListDto> SocialMedias) : ICommand;