using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PetProject.Domain.Volunteers;
public class PetInformation
{
    public TelephonNumber ContactDetails { get; private set; }
    public SpeciesId SpeciesId { get; }
    public BreedId BreedId { get; }
}
