using CSharpFunctionalExtensions;

namespace PetProject.SharedKernel.ValueObjects;

public record SpeciesId
{
    //ef core
    public SpeciesId()
    {
        
    }
    public SpeciesId(Guid value)
    {
        Value = value;
    }
    public Guid Value { get; }
    public static SpeciesId NewSpeciesId() => new(Guid.NewGuid());
    public static SpeciesId Empty() => new(Guid.Empty);
    public static Result<SpeciesId, string> Create(Guid value)
    {
        if (value == Guid.Empty)
            return "Id cannot be empty";

        return new SpeciesId(value);
    }
}
