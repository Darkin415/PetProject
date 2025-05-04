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
        public SocialMedia(string title, string linkMedia)
        {
            Title = title;
            LinkMedia = linkMedia;
        }
        public string Title { get; }
        public string LinkMedia { get; }
        public static Result<SocialMedia> Create(string title, string linkMedia)
        {
            if(string.IsNullOrWhiteSpace(title))
            {
                return Result.Failure<SocialMedia>("Title can not be empty");
            }
            if (string.IsNullOrWhiteSpace(linkMedia))
            {
                return Result.Failure<SocialMedia>("Linkmedia can not be empty");
            }
            else
            {
                var socialmedia = new SocialMedia(title, linkMedia);
                return Result.Success(new SocialMedia(socialmedia));
            }
        }
    }
}
