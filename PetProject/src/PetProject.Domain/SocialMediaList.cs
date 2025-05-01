using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Domain
{
    public record SocialMediaList(IReadOnlyList<SocialMedia> SocialMedias)
    {
        public SocialMediaList() : this(new List<SocialMedia>())
        {

        }
    }
    
}
