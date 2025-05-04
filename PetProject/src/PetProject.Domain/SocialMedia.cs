using PetProject.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PetProject.Domain;
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
        if (string.IsNullOrWhiteSpace(title))
        {
            return "Title can not be empty";
        }
        if (string.IsNullOrWhiteSpace(linkMedia))
        {
            return "Linkmedia can not be empty";
        }
        return new SocialMedia(title, linkMedia);
    }
}
