using CSharpFunctionalExtensions;
using PetProject.Domain.Shared.ValueObject;
namespace PetProject.Domain.Volunteers;

public record Photos
{
    public Photos(string pathToStorage)
    {
        PathToStorage = pathToStorage;
    }

    public string PathToStorage { get; }

    public static Result<Photos, Error> Create(string pathToStorage)
    {
        if (string.IsNullOrWhiteSpace(pathToStorage))

            return Errors.General.ValueIsInvalid("Path");
     
        return new Photos(pathToStorage);
    }
}
