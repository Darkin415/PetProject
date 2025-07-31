using CSharpFunctionalExtensions;
using PetProject.Contracts;

namespace PetProject.Volunteers.Domain.Pets;

public record CastrationStatus
{
    public CastrationStatus(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<CastrationStatus, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid("Castration status");

        return new CastrationStatus(value);
    }
}
