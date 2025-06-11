using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObject;
namespace PetProject.Domain.Volunteers;
public class PetInformation
{
    public TelephonNumber ContactDetails { get; private set; }
    public SpeciesId SpeciesId { get; }
    public BreedId BreedId { get; }
}
