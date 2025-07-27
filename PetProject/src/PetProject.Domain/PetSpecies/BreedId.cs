using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetProject.Domain.Shared.ValueObjects;

namespace PetProject.Domain.PetSpecies;

public record BreedId
{
    public BreedId()
    {
        
    }
    public BreedId(Guid value)
    {
        Value = value;
    }
    public Guid Value { get; }
    public static BreedId NewBreedId() => new(Guid.NewGuid());
    public static BreedId Empty() => new(Guid.Empty);
    public static Result<BreedId, Error> Create(Guid value)
    {
        if (value == Guid.Empty)
            return Error.Failure("value.invalid", "Value can not be empty");

        return new BreedId(value);
    }
}
