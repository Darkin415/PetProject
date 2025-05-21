using CSharpFunctionalExtensions;
using PetProject.Domain.Shared.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PetProject.Domain.Volunteers;
public record SocialMedia
{
    public SocialMedia(string title, string linkMedia)
    {
        Title = title;
        LinkMedia = linkMedia;
    }
    public string Title { get; }
    public string LinkMedia { get; }
    public static Result<SocialMedia, Error> Create(string title, string linkMedia)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return Errors.General.ValueIsInvalid("Title");
        }
        if (string.IsNullOrWhiteSpace(linkMedia))
        {
            return Errors.General.ValueIsInvalid("Link media");
        }
        return new SocialMedia(title, linkMedia);
    }
}
