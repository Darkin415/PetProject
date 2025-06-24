using PetProject.Domain.Shared;
using PetProject.Domain.Shared.Ids;

namespace PetProject.Domain.PetSpecies;

public class Species : Entity<SpeciesId>
{
    private List<Breed> _breeds = [];

    // для ef core
    private Species(SpeciesId id)
        :base(id)
    {      
    }
 
    public Species(SpeciesId id, string name, List<Breed> breeds) : base(id)
    {
         Name = name;
        _breeds = breeds;
    }

    public string Name { get; private set; }

    public IReadOnlyCollection<Breed> Breeds => _breeds.AsReadOnly();

}
