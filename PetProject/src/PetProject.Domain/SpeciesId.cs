using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace PetProject.Domain
{
    public record SpeciesId
    {
        public Guid Value { get; }
        public SpeciesId(Guid value)
        {
            Value = value;
        }
        public static SpeciesId NewModuleId() => new(Guid.NewGuid());
        public static SpeciesId Empty() => new(Guid.Empty);

        public static Result<SpeciesId> Create(Guid value)
        {
            if (value == Guid.Empty)
                return Result.Failure<SpeciesId>("Id cannot be empty");

            return Result.Success(new SpeciesId(value));
        }
    }
}
