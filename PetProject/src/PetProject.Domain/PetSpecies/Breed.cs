using CSharpFunctionalExtensions;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObjects;

namespace PetProject.Domain.PetSpecies;

public class Breed : Shared.Entity<BreedId>
{
    // для ef core
    private Breed(BreedId id) : base(id)
    {
        
    }

    public BreedId Id { get; private set; }
    
    public static Result<Breed, Error> Create(BreedId id)
    {
        return new Breed(id);
    }

}
