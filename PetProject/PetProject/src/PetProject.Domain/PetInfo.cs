using CSharpFunctionalExtensions;
using PetProject.Domain.PetSpecies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Domain;

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
