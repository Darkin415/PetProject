using PetProject.Contracts.Command;
using PetProject.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Contracts.Request;

public record CreateVolunteerRequest(
    string Title,
    string LinkMedia,
    string Information,
    string Email,
    string PhoneNumber,
    CreateFullNameDto FullName,
    IEnumerable<SocialMediaDto> SocialMedias)
{
    public AddVolunteerCommand ToCommand() => new (
        Title, 
        LinkMedia, 
        Information, 
        Email, 
        PhoneNumber, 
        FullName, 
        SocialMedias);        
}




