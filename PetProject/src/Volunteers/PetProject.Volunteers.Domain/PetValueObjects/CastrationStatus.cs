using CSharpFunctionalExtensions;
using PetProject.SharedKernel;

namespace PetProject.Volunteers.Domain.PetValueObjects;

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
