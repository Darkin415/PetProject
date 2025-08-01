using CSharpFunctionalExtensions;
using PetProject.SharedKernel;

namespace PetProject.Contracts.ValueObjects;

public record Description
{
    public Description(string information)
    {
        Information = information;
    }
    public string Information { get; }
    public static Result<Description, Error> Create(string information)
    {
        if (string.IsNullOrWhiteSpace(information))
            return Errors.General.ValueIsInvalid("Information");

        return new Description(information);
    }
}
