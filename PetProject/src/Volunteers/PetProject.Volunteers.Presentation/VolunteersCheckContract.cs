using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetProejct.Volunteers.Application;
using PetProject.SharedKernel;
using PetProject.Volunteers.Contracts;

namespace PetProject.Volunteers.Presentation;

public class VolunteersCheckContract(IVolunteersReadDbContext readDbContext) : IPetCheckContract
{
    public async Task<Result<bool, Error>> IsBreedUsedInPetsAsync(Guid breedId, CancellationToken cancellationToken = default)
    {
        var breedIfExist = await readDbContext.Pets.AnyAsync(p => p.BreedId == breedId, cancellationToken);
        if(breedIfExist)
            return Error.Conflict("Breed.already.exists", "Breed already exists");
        
        return Result.Success<bool, Error>(breedIfExist);
    }

    public async Task<Result<bool, Error>> IsSpeciesUsedInPetsAsync(Guid breedId, CancellationToken cancellationToken = default)
    {
        var speciesExist = await readDbContext.Pets.AnyAsync(p => p.BreedId == breedId, cancellationToken);
        if(speciesExist)
            return Error.Conflict("Species.already.exists", "Species already exists");
        
        return Result.Success<bool, Error>(speciesExist);
    }
}