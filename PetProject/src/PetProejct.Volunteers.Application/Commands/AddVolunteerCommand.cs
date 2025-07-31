using PetProject.Contracts.Abstraction;
using PetProject.Contracts.Dtos;

namespace PetProejct.Volunteers.Application.Commands;

public record AddVolunteerCommand(string Title,
    string LinkMedia,
    string Information,
    string Email,
    string PhoneNumber,
    CreateFullNameDto FullName,
    IEnumerable<SocialMediaDto> SocialMedias) :ICommand;
