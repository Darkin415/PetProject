using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
namespace PetProject.Domain
{
    public record BreedId
    {
        public BreedId(Guid value)
        {
            Value = value;
        }
        public Guid Value { get; }
        public static BreedId NewGuidId() => new(Guid.NewGuid());
        public static BreedId Empty() => new(Guid.Empty);
        public static Result<BreedId> Create(Guid value)
        {
            if (value == Guid.Empty)
                return Result.Failure<BreedId>("Id cannot be empty");
            else
            {
                var id = new BreedId(value);
                return Result.Success(new BreedId(value));
            }
        }
    }
}
