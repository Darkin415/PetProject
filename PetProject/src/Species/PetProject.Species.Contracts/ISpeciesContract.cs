
using CSharpFunctionalExtensions;

using PetProject.SharedKernel;
using PetProject.SharedKernel.ValueObjects;
using PetProject.Species.Application.Species;
using PetProject.Species.Contracts.DTOs;
using PetProject.Species.Contracts.Models;
using PetProject.Species.Domain.PetSpecies;

namespace PetProject.Species.Contracts;

public interface ISpeciesContract
{
    Task<Result<BreedDto, Error>> GetBreedByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<Result<Guid, ErrorList>> AddSpecies(CreateSpeciesRequest species, CancellationToken cancellationToken);

    Task<Result<SpeciesDto, Error>> GetSpeciesByNameAsync(string title, CancellationToken cancellationToken);

    Task<Result<BreedDto, Error>> GetBreedByNameAsync(string title, CancellationToken cancellationToken);
    
    Task<Result<List<BreedDto>, ErrorList>> GetBreedsBySpeciesId(Guid speciesId, CancellationToken cancellationToken);
    
    Task<PagedList<SpeciesDto>> GetSpeciesWithPaginationAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken);
    

}
