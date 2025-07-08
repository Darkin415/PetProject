using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace PetProject.Domain.Shared.ValueObjects;

public record TelephonNumber
{
    public string Value { get; }

    private TelephonNumber(string value)
    {
        Value = value;
    }

    public static Result<TelephonNumber, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Errors.General.ValueIsInvalid();
        }

        var normalizedNumber = value.Trim();

        if (!IsValidRussianPhoneNumber(normalizedNumber))
        {
            return Errors.General.ValueIsInvalid("Invalid format telephon number");
        }

        return new TelephonNumber(normalizedNumber);
    }

    private static bool IsValidRussianPhoneNumber(string phoneNumber)
    {

        var digitsOnly = Regex.Replace(phoneNumber, @"[^\d+]", "");


        if (digitsOnly.StartsWith("+7") && digitsOnly.Length == 12)
        {
            return true;
        }

        if (digitsOnly.StartsWith("8") && digitsOnly.Length == 11)
        {
            return true;
        }

        if (digitsOnly.StartsWith("7") && digitsOnly.Length == 11)
        {
            return true;
        }

        return false;
    }
}