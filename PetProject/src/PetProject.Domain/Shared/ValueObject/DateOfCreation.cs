using CSharpFunctionalExtensions;

namespace PetProject.Domain.Shared.ValueObject;

public record DateOfCreation
{
    public DateOfCreation()
    {
        
    }
    public DateOfCreation(DateTime dateOfCreation)
    {
        CreationDate = dateOfCreation;
    }

    public DateTime CreationDate { get; }

    public static Result<DateOfCreation, Error> Create(DateTime dateOfCreation)
    {
        if (dateOfCreation < DateTime.UtcNow.AddYears(-30))
            return Errors.General.ValueIsInvalid("Birth date");

        return new DateOfCreation(dateOfCreation);
    }
}