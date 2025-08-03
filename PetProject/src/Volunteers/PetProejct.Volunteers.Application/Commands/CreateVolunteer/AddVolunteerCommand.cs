using PetProject.Core.Abstraction;
using PetProject.Core.DTOs;
using PetProject.Files.Contracts.Dtos;
using PetProject.Volunteers.Contracts.DTOs;

namespace PetProejct.Volunteers.Application.Commands.CreateVolunteer;

public record AddVolunteerCommand(string Title,
    string LinkMedia,
    string Information,
    string Email,
    string PhoneNumber,
    CreateFullNameDto FullName,
    IEnumerable<SocialMediaDto> SocialMedias) :ICommand;
