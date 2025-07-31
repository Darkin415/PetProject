using CSharpFunctionalExtensions;
using PetProject.Contracts;
using PetProject.Species.Domain.PetSpecies;

namespace PetProejct.Volunteers.Application;

public interface ISpeciesRepository
{
    Task<Result<Species, Error>> GetSpeciesByIdAsync(SpeciesId id, CancellationToken cancellationToken);

    Task<Result<Breed, Error>> GetBreedByIdAsync(BreedId id, CancellationToken cancellationToken);

    Task<Result<Guid, ErrorList>> AddSpecies(Species species, CancellationToken cancellationToken);

    Task<Result<Species, Error>> GetSpeciesByNameAsync(Title title, CancellationToken cancellationToken);

    Task<Result<Breed, Error>> GetBreedByNameAsync(Title title, CancellationToken cancellationToken);

    Task<Result<Guid, ErrorList>> AddBreed(Breed breed, CancellationToken cancellationToken);

    Guid DeleteBreed(Breed breed, CancellationToken cancellationToken);

    Guid DeleteSpecies(Species species, CancellationToken cancellationToken);

    Task<Result<List<Breed>, Error>> GetBreedsBySpeciesId(SpeciesId speciesId, CancellationToken cancellationToken);
    
    






}
