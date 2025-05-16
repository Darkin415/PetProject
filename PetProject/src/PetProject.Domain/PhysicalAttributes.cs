using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;

namespace PetProject.Domain;

public record PhysicalAttributes
{
    public PhysicalAttributes(double weight, double height)
    {
        Weight = weight;
        Height = height;
    }

    public double Weight { get; }

    public double Height { get; }

    public static Result<PhysicalAttributes, Error> Create(double weight, double height)
    {
        if (weight <= 0)
            return Errors.General.ValueIsInvalid("Weight");
        if (height <= 0)
            return Errors.General.ValueIsInvalid("Height");

        return new PhysicalAttributes(weight, height);
    }
}
