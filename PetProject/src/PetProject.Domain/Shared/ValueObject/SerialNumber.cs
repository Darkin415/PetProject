using CSharpFunctionalExtensions;

namespace PetProject.Domain.Shared.ValueObject;

public record SerialNumber
{
    public static SerialNumber First => new(1);
    private SerialNumber(int value)
    {
        Value = value;
    }
    public int Value { get; }

    public static Result<SerialNumber, Error> Create(int number)
    {
        if (number <= 0)
            return Errors.General.ValueIsInvalid("serial number");

        return new SerialNumber(number);
    }
}