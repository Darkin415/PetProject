using PetProject.Domain.Shared;
using PetProject.Domain.Shared.Ids;

namespace PetProject.Domain.PetSpecies;

public class Breed : Entity<BreedId>
{
    // для ef core
    private Breed(BreedId id) : base(id)
    {
        
    }
    public Breed(BreedId id, string title) : base(id)
    {
        Title = title;
    }
   
    public string Title { get; set; }

    public BreedId Id { get; private set; }

}
