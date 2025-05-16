using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;

namespace PetProject.Domain;

public record VaccinationStatus
{
    public VaccinationStatus(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<VaccinationStatus, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid("Vaccination status");

        return new VaccinationStatus(value);
    }
}