using CSharpFunctionalExtensions;
using PetProject.Contracts;
using PetProject.SharedKernel;

namespace PetProject.Volunteers.Domain.PetValueObjects;

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