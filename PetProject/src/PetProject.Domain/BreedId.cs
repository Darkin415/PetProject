using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Domain
{
    public record BreedId
    {
        private BreedId(Guid value)
        {
            Value = value;
        }
        public Guid Value { get; }

        public static BreedId NewGuidId() => new(Guid.NewGuid());
        public static BreedId Empty() => new(Guid.Empty);

    }
}
