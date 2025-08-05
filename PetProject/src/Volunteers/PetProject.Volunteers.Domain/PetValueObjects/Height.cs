using CSharpFunctionalExtensions;
using PetProject.SharedKernel;

namespace PetProject.Volunteers.Domain.PetValueObjects;

public record Height
{
    /// <summary>
    /// для ef core
    /// </summary>
    private Height()
    {
        
    }
    public Height(double value)
    {
        Value = value;
    }

    public double? Value { get; }

    public static Result<Height, Error> Create(double? value)
    {
        if (value == null)
            return Errors.General.ValueIsInvalid("Height");

        return new Height(value.Value);
    }
}