using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Domain
{
    public record SpeciesId
    {
        private SpeciesId(Guid value)
        {
            Value = value;
        }
        public Guid Value { get; }
        public static SpeciesId NewModuleId() => new(Guid.NewGuid());
        public static SpeciesId Empty() => new(Guid.Empty);


    }
}
