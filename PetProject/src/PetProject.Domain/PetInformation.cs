using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Domain
{
    public class PetInformation
    {
        public TelephonNumber ContactDetails { get; private set; }
        public SpeciesId SpeciesId { get; }
        public BreedId BreedId { get; }
    }
}
