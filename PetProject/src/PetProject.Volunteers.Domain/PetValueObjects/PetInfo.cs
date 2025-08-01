using CSharpFunctionalExtensions;
using PetProject.Contracts;
using PetProject.SharedKernel.ValueObjects;

namespace PetProject.Volunteers.Domain.PetValueObjects;

public record PetInfo
{
    public PetInfo()
    {
        
    }
    public SpeciesId SpeciesId { get; }
    public BreedId BreedId { get; }

    private PetInfo(SpeciesId speciesId, BreedId breedId)
    {
        SpeciesId = speciesId;
        BreedId = breedId;
    }

    public static Result<PetInfo, string> Create(SpeciesId speciesId, BreedId breedId)
    {
        if (speciesId == null)
            return "SpeciesId cannot be null";

        if (breedId == null)
            return "BreedId cannot be null";

        return new PetInfo(speciesId, breedId);
    }
}
