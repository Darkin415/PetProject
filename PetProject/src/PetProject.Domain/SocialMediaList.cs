using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

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
        public static Result<SocialMediaList> Create(IReadOnlyList<SocialMedia> socialMedias)
        {
            if (socialMedias == null)
            {
                return Result.Failure<SocialMediaList>("SocialMedias can not be null");
            }
            else
            {
                var res = new SocialMediaList(socialMedias);
                return Result.Success(new SocialMediaList(socialMedias));
            }
        }
    }
}
