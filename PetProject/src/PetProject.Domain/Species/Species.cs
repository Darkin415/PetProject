using PetProject.Domain.Shared.Ids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Domain.Species
{
    public class Species
    {
        public SpeciesId Id { get; private set; }
        public string Title { get; private set; }
        public List<Breed> Breeds { get; private set; } = [];
        public Species(string title)
        {   
            Title = title;
        }
    }
}
