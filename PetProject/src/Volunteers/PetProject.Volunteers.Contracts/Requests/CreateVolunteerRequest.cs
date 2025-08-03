//using PetProject.Contracts.Commands;

using PetProject.Core.DTOs;
using PetProject.Files.Contracts.Dtos;
using PetProject.Volunteers.Contracts.DTOs;

namespace PetProject.Volunteers.Contracts.Requests;

public record CreateVolunteerRequest(
    string Title,
    string LinkMedia,
    string Information,
    string Email,
    string PhoneNumber,
    CreateFullNameDto FullName,
    IEnumerable<SocialMediaDto> SocialMedias);
//{
//    public AddVolunteerCommand ToCommand() => new(
//        Title,
//        LinkMedia,
//        Information,
//        Email,
//        PhoneNumber,
//        FullName,
//        SocialMedias);
//}




