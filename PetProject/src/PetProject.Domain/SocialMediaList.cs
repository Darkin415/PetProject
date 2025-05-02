using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Domain
{
    public record SocialMediaList
    {
        public IReadOnlyList<SocialMedia> SocialMedias { get; }
        private SocialMediaList()
        {
            
        }       
        public SocialMediaList(IReadOnlyList<SocialMedia> socialMedias)
        {
            SocialMedias = socialMedias;
        }
    }
}
