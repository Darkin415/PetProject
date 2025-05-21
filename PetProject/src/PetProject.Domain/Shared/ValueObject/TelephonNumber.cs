using CSharpFunctionalExtensions;

namespace PetProject.Domain.Shared.ValueObject;

public record TelephonNumber
{
    public string Value { get; }
    public TelephonNumber(string value)
    {
        Value = value;
    }
    public static Result<TelephonNumber, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Errors.General.ValueIsInvalid();

        }
        return new TelephonNumber(value);
    }
}