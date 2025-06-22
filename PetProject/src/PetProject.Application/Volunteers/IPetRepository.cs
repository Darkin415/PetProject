using CSharpFunctionalExtensions;
using PetProject.Domain.PetSpecies;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PetProject.Domain.Shared.ValueObject.Errors;

namespace PetProject.Application.Volunteers;

public interface ISpeciesRepository
{
    Task<Result<Species, Error>> GetSpeciesAsync(SpeciesId id, CancellationToken cancellationToken);

    Task<Result<Breed, Error>> GetBreedAsync(BreedId id, CancellationToken cancellationToken);

}
