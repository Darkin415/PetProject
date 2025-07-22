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
 
   

   
    
    public IReadOnlyCollection<Breed> Breeds => _breeds.AsReadOnly();
    
    public static Result<Species, Error> Create(SpeciesId id)
    {
        
        return new Species(id);
    }

}
