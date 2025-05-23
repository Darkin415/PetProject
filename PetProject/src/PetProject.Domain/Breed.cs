using CSharpFunctionalExtensions;
using PetProject.Domain.Shared.ValueObject;

namespace PetProject.Domain;

public record Breed
{
    public Breed(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public static Result<Breed, Error> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsInvalid("Breed");

        return new Breed(name);
    }
}
