using CSharpFunctionalExtensions;
using PetProject.Contracts;

namespace PetProject.Species.Domain.PetSpecies;

public class Species : Contracts.Entity<SpeciesId>
{
    private List<Breed> _breeds = [];

    // для ef core
    private Species(SpeciesId id)
        :base(id)
    {      
    }


    public Species(SpeciesId id, Title title) : base(id)
    {
        Title = title;
    }
   
    public Title Title { get; private set; }
    public IReadOnlyCollection<Breed> Breeds => _breeds.AsReadOnly();
    
    public static Result<Species, Error> Create(SpeciesId id)
    {
        
        return new Species(id);
    }

}
