using CSharpFunctionalExtensions;
using Microsoft.VisualBasic;

namespace PetProject.Domain.Shared.ValueObject;

public record BirthDay
{
    public BirthDay(DateTime birthDate)
    {
        BirthDate = birthDate;
    }
    
    public DateTime BirthDate { get; }

    public static Result<BirthDay, Error> Create(DateTime birthDate)
    {
        if (birthDate < DateTime.UtcNow.AddYears(-30))
            return Errors.General.ValueIsInvalid("Birth date");

        return new BirthDay(birthDate);
    }
}



