using CSharpFunctionalExtensions;
using PetProject.Contracts;
using PetProject.SharedKernel;

namespace PetProject.Volunteers.Domain.PetValueObjects;

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
