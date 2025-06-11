using CSharpFunctionalExtensions;

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
