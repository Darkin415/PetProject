using CSharpFunctionalExtensions;
using PetProject.SharedKernel;

namespace PetProject.Contracts.ValueObjects;

public record FullName
{
    public string FirstName { get; }
    public string LastName { get; }
    public string Surname { get; }
    private FullName(string firstName, string lastName, string surname)
    {
        FirstName = firstName;
        LastName = lastName;
        Surname = surname;
    }
    public static Result<FullName, Error> Create(string firstName, string lastName, string surname)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Errors.General.ValueIsInvalid("Firstname");

        if (string.IsNullOrWhiteSpace(lastName))
            return Errors.General.ValueIsInvalid("Lastname");

        if (string.IsNullOrWhiteSpace(surname))
            return Errors.General.ValueIsInvalid("Surname");

        return new FullName(firstName, lastName, surname);
    }
    
}

