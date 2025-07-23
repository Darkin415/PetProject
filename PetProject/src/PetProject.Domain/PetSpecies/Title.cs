using CSharpFunctionalExtensions;
using PetProject.Domain.Shared.ValueObjects;

namespace PetProject.Domain.PetSpecies;

public record Title
{
    /// <summary>
    /// для ef core
    /// </summary>
    private Title()
    {
        
    }
    public Title(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public static Result<Title, Error> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsInvalid("Name");

        return new Title(name);
    }
}