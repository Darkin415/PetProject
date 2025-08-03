
using CSharpFunctionalExtensions;

using PetProject.SharedKernel;
using PetProject.SharedKernel.ValueObjects;
using PetProject.Species.Application.Breed;
using PetProject.Species.Application.Species;
using PetProject.Species.Contracts.DTOs;
using PetProject.Species.Domain.PetSpecies;

namespace PetProject.Species.Contracts;

public interface ISpeciesContract
{
    Task<Result<BreedDto, Error>> GetBreedByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<Result<Guid, ErrorList>> AddSpecies(CreateSpeciesRequest species, CancellationToken cancellationToken);

    Task<Result<Domain.PetSpecies.Species, Error>> GetSpeciesByNameAsync(string title, CancellationToken cancellationToken);

    Task<Result<Domain.PetSpecies.Breed, Error>> GetBreedByNameAsync(string title, CancellationToken cancellationToken);

    Task<Result<Guid, ErrorList>> AddBreed(CreateBreedRequest createBreedRequest, CancellationToken cancellationToken);

    Guid DeleteBreed(Guid id, CancellationToken cancellationToken);
    
    
    






}
