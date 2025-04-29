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

        private SocialMedia(int value)
        {
            Value = value;
        }
        public int Value { get; }
        public string Title { get; } 
        public string LinkMedia { get; }
    }
}
