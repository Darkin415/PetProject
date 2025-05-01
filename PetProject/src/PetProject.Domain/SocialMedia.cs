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

        public SocialMedia(string title, string linkMedia )
        {
            Title = title;
            LinkMedia = linkMedia;
        }
        
        public string Title { get; private set; } 
        public string LinkMedia { get; private set; }
    }
}
