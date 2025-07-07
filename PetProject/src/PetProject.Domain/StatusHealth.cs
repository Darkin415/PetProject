using CSharpFunctionalExtensions;
using PetProject.Domain.Shared.ValueObjects;

namespace PetProject.Domain;

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
