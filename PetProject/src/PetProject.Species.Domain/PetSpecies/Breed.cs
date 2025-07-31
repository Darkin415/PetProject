using CSharpFunctionalExtensions;
using PetProject.Contracts;

namespace PetProject.Species.Domain.PetSpecies;

public class Breed : Contracts.Entity<BreedId>
{
    // для ef core
    private Breed(BreedId id) : base(id)
    {
        
    }

    public Breed(SpeciesId speciesId, BreedId id, Title title) : base(id)
    {
        Title = title;
        SpeciesId = speciesId;

    }
    public Title Title { get; private set; }

    public SpeciesId SpeciesId { get; private set; }
    
    public static Result<Breed, Error> Create(BreedId id)
    {
        return new Breed(id);
    }

}

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