using CSharpFunctionalExtensions;
using PetProject.SharedKernel;
using PetProject.SharedKernel.ValueObjects;

namespace PetProject.Species.Domain.PetSpecies;

public class Breed : SharedKernel.Entity<BreedId>
{
    // для ef core
    public Breed(BreedId id) : base(id)
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