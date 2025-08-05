using CSharpFunctionalExtensions;
using PetProject.SharedKernel;

namespace PetProject.Volunteers.Domain.PetValueObjects;

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