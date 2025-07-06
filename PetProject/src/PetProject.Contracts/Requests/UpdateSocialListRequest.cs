using PetProject.Contracts.Command;
using PetProject.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Contracts.Request;

public record UpdateSocialListRequest(IEnumerable<SocialListDto> SocialMedias)
{
    public UpdateSocialNetworksCommand ToCommmand(Guid id) => new(id, SocialMedias);
}


