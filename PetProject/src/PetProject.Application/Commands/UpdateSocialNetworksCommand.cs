using PetProject.Contracts.Dtos;

namespace PetProject.Application.Commands;

public record UpdateSocialNetworksCommand(Guid VolunteerId, IEnumerable<SocialListDto> SocialMedias) : ICommand;