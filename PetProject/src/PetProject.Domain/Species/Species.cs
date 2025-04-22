using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Domain.Species
{
    public class Species
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }

        public List<Breed> Breeds = [];





    }
}
