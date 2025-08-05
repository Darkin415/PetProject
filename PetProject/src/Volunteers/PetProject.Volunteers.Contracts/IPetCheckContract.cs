using CSharpFunctionalExtensions;
using PetProject.SharedKernel;

namespace PetProject.Volunteers.Contracts;

public interface IPetCheckContract
{
    Task<Result<bool, Error>> IsBreedUsedInPetsAsync(Guid breedId, CancellationToken cancellationToken = default);
    
    Task<Result<bool, Error>> IsSpeciesUsedInPetsAsync(Guid breedId, CancellationToken cancellationToken = default);
}