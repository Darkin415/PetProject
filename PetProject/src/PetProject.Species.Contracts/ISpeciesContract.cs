using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;
using PetProject.Contracts;
using PetProject.SharedKernel;
using PetProject.SharedKernel.ValueObjects;
using PetProject.Species.Domain.PetSpecies;

namespace PetProject.Species.Contracts;

public interface ISpeciesContract
{
    Task<Result<Domain.PetSpecies.Species, Error>> GetSpeciesByIdAsync(SpeciesId id, CancellationToken cancellationToken);

    Task<Result<Domain.PetSpecies.Breed, Error>> GetBreedByIdAsync(BreedId id, CancellationToken cancellationToken);

    Task<Result<Guid, ErrorList>> AddSpecies(Domain.PetSpecies.Species species, CancellationToken cancellationToken);

    Task<Result<Domain.PetSpecies.Species, Error>> GetSpeciesByNameAsync(Title title, CancellationToken cancellationToken);

    Task<Result<Domain.PetSpecies.Breed, Error>> GetBreedByNameAsync(Title title, CancellationToken cancellationToken);

    Task<Result<Guid, ErrorList>> AddBreed(Domain.PetSpecies.Breed breed, CancellationToken cancellationToken);

    Guid DeleteBreed(Domain.PetSpecies.Breed breed, CancellationToken cancellationToken);

    Guid DeleteSpecies(Domain.PetSpecies.Species species, CancellationToken cancellationToken);

    Task<Result<List<Domain.PetSpecies.Breed>, Error>> GetBreedsBySpeciesId(SpeciesId speciesId, CancellationToken cancellationToken);
    
    






}
