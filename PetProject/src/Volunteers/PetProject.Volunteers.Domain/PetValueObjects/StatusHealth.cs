using CSharpFunctionalExtensions;
using PetProject.SharedKernel;

namespace PetProject.Volunteers.Domain.PetValueObjects;

public record StatusHealth
{
    public StatusHealth(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<StatusHealth, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid("Status health");

        return new StatusHealth(value);
    }
}
