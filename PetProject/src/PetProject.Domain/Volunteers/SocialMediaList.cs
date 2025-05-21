using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetProject.Domain.Shared;

namespace PetProject.Domain.Volunteers;

public record SocialMediaList
{
    private SocialMediaList()
    {

    }
    public SocialMediaList(IEnumerable<SocialMedia> socialMedias)
    {
        SocialMedias = socialMedias;
    }
    public IEnumerable<SocialMedia> SocialMedias { get; }
}

