using CSharpFunctionalExtensions;
using PetProject.Domain.Shared.ValueObjects;

namespace PetProject.Domain.Volunteers;

public record Weight
{
    /// <summary>
    /// для ef core
    /// </summary>
    private Weight()
    {
        
    }
    public Weight(double value)
    {
        Value = value;
    }

    public double? Value { get; }

    public static Result<Weight, Error> Create(double? value)
    {
        if (value == null)
            return Errors.General.ValueIsInvalid("Weight");

        return new Weight(value.Value);
    }
}

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
 