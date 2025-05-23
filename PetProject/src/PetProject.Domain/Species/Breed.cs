using PetProject.Domain.Shared.Ids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Domain.Species
{
    public  class Breed
    {
        public BreedId Id { get; set; }
        public string Title { get; set; }
        private Breed(BreedId BreedId, string title) 
        {
            Title = title;
        }
    }
}
