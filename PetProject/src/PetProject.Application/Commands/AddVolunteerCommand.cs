using PetProject.Contracts.Dtos;

namespace PetProject.Application.Commands;

public record AddVolunteerCommand(string Title,
    string LinkMedia,
    string Information,
    string Email,
    string PhoneNumber,
    CreateFullNameDto FullName,
    IEnumerable<SocialMediaDto> SocialMedias) :ICommand;
