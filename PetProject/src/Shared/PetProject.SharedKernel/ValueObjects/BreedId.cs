using CSharpFunctionalExtensions;

namespace PetProject.SharedKernel.ValueObjects;

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


