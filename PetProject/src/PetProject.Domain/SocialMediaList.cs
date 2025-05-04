using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetProject.Domain.Shared;

namespace PetProject.Domain;

public record SocialMediaList
{
    public IEnumerable<SocialMedia> SocialMedias { get; }
    private SocialMediaList()
    {

    }
    public SocialMediaList(IEnumerable<SocialMedia> socialMedias)
    {
        SocialMedias = socialMedias;
    }

}

