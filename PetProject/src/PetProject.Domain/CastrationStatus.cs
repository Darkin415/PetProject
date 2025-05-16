using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;

namespace PetProject.Domain;

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