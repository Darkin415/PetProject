using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;

namespace PetProject.Domain.Shared.Ids;

public record SpeciesId
{
    public Guid Value { get; }
    public SpeciesId(Guid value)
    {
        Value = value;
    }
    public static SpeciesId NewSpeciesId() => new(Guid.NewGuid());
    public static SpeciesId Empty() => new(Guid.Empty);

    public static Result<SpeciesId> Create(Guid value)
    {
        return new SpeciesId(value);
    }
}
