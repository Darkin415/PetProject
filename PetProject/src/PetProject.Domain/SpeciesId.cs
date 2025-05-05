using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetProject.Domain.Shared;

namespace PetProject.Domain;

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
        return new SpeciesId(value);
    }
}
