using CSharpFunctionalExtensions;
using PetProject.Domain.PetSpecies;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PetProject.Domain.Shared.ValueObjects.Errors;

namespace PetProject.Application.Volunteers;

public interface ISpeciesRepository
{
    Task<Result<Species, Error>> GetSpeciesByIdAsync(SpeciesId id, CancellationToken cancellationToken);

    Task<Result<Breed, Error>> GetBreedByIdAsync(BreedId id, CancellationToken cancellationToken);

    Task<Result<Guid, ErrorList>> CreateSpecies(string title, CancellationToken cancellationToken);

    Task<Result<Species, Error>> GetSpeciesByNameAsync(Title title, CancellationToken cancellationToken);

    Task<Result<Breed, Error>> GetBreedByNameAsync(Title title, CancellationToken cancellationToken);

    Task<Result<Guid, ErrorList>> CreateBreed(SpeciesId speciesId, string title, CancellationToken cancellationToken);

    Guid DeleteBreed(Breed breed, CancellationToken cancellationToken);

    Guid DeleteSpecies(Species species, CancellationToken cancellationToken);

    Task<Result<List<Breed>, Error>> GetBreedsBySpeciesId(SpeciesId speciesId);






}
