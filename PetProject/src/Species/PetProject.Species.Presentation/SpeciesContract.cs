using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetProject.SharedKernel;
using PetProject.SharedKernel.ValueObjects;
using PetProject.Species.Application;
using PetProject.Species.Application.Breed;
using PetProject.Species.Application.Species;
using PetProject.Species.Contracts;
using PetProject.Species.Contracts.DTOs;
using PetProject.Species.Domain.PetSpecies;

namespace PetProject.Species.Presentation;

public class SpeciesContract(ISpeciesReadDbContext readDbContext) : ISpeciesContract
{
    public async Task<Result<BreedDto, Error>> GetBreedByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var breed = await readDbContext.Breeds.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        return breed;
    }

    public Task<Result<Guid, ErrorList>> AddSpecies(CreateSpeciesRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Domain.PetSpecies.Species, Error>> GetSpeciesByNameAsync(Title title, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Breed, Error>> GetBreedByNameAsync(Title title, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Guid, ErrorList>> AddBreed(CreateBreedRequest createBreedRequest, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Guid DeleteBreed(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}