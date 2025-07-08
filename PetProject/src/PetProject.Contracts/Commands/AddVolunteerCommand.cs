using PetProject.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Contracts.Commands;

public record AddVolunteerCommand(string Title,
    string LinkMedia,
    string Information,
    string Email,
    string PhoneNumber,
    CreateFullNameDto FullName,
    IEnumerable<SocialMediaDto> SocialMedias);
