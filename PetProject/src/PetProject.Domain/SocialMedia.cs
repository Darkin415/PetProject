using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Domain
{
    public record SocialMedia
    {
        public List<SocialMedia> _socialMedias { get; private set; }
        public SocialMedia(string title, string linkMedia)
        {
            LinkMedia = linkMedia;
            Title = title;
        }
        public string Title { get; } 
        public string LinkMedia { get; }

        public static Result<SocialMedia> Create(string title, string linkMedia)
        {
            if (string.IsNullOrWhiteSpace(title))
                return Result.Failure<SocialMedia>("Title cannot be empty");

            if (string.IsNullOrWhiteSpace(linkMedia))
                return Result.Failure<SocialMedia>("LinkMedia cannot be empty");

            return new SocialMedia(title, linkMedia);
        }



    }
}
