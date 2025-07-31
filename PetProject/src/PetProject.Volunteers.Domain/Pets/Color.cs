using CSharpFunctionalExtensions;
using PetProject.Contracts;

namespace PetProject.Volunteers.Domain.Pets;

public record Color
{
    public Color(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Color, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid("Value");

        return new Color(value);
    }
}
