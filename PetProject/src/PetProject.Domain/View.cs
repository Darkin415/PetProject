using CSharpFunctionalExtensions;
using PetProject.Domain.Shared.ValueObject;

namespace PetProject.Domain;

public record View  
{
    public View(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<View, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid("View");

        return new View(value);
    }
}
