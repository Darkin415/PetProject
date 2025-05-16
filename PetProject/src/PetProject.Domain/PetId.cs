using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;

namespace PetProject.Domain;

public record PetId
{
    public PetId(Guid value)
    {
        Value = value;
    }
    public Guid Value { get; }
    public static PetId NewGuidId() => new(Guid.NewGuid());
    public static PetId Empty() => new(Guid.Empty);
    public static Result<PetId, string> Create(Guid value)
    {
        if (value == Guid.Empty)
            return "Id cannot be empty";
        
        return new PetId(value);
    }
}