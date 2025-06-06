using PetProject.Domain.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Application.Volunteers.Create.SocialList;

public record UpdateSocialListRequest(Guid VolunteerId, IEnumerable<SocialListDto> SocialMedias);

public record SocialListDto(string Title, string LinkMedia);