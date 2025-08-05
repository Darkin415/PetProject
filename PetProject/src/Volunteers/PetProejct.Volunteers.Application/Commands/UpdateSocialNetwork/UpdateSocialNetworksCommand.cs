
using PetProject.Core.Abstraction;
using PetProject.Core.DTOs;
using PetProject.Files.Contracts.Dtos;
using PetProject.Volunteers.Contracts.DTOs;

namespace PetProejct.Volunteers.Application.Commands.UpdateSocialNetwork;

public record UpdateSocialNetworksCommand(Guid VolunteerId, IEnumerable<SocialListDto> SocialMedias) : ICommand;