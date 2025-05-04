using PetProject.Domain.Shared;

namespace PetProject.Domain;

public record TelephonNumber
{
    public string Value { get; }
    public TelephonNumber(string value)
    {
        Value = value;
    }
    public static Result<TelephonNumber> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return "Number can not be empty";

        }
        return new TelephonNumber(value);
    }
}