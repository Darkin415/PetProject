using CSharpFunctionalExtensions;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObjects;

namespace PetProject.Domain.PetSpecies;

public class Species : Shared.Entity<SpeciesId>
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