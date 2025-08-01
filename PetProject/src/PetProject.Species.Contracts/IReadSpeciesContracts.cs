using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;
using PetProject.Contracts;
using PetProject.SharedKernel;
using PetProject.SharedKernel.ValueObjects;
using PetProject.Species.Domain.PetSpecies;

namespace PetProject.Species.Contracts;

public interface IReadSpeciesContracts
{
    Task<Result<Breed, Error>> GetBreedByIdAsync(BreedId id, CancellationToken cancellationToken);
    
    Guid DeleteBreed(Breed breed, CancellationToken cancellationToken);
}