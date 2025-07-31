using CSharpFunctionalExtensions;
using PetProject.Contracts;

namespace PetProject.Volunteers.Domain.Pets;

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
