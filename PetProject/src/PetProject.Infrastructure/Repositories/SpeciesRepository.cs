using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetProject.Application.Volunteers;
using PetProject.Domain.PetSpecies;
using PetProject.Domain.Shared.ValueObjects;
using PetProject.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PetProject.Infrastructure.Repositories;

public class SpeciesRepository : ISpeciesRepository
{
    private readonly WriteDbContext _dbContext;
    public SpeciesRepository(WriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Species, Error>> GetSpeciesAsync(SpeciesId id, CancellationToken cancellationToken)
    {
        var species = await _dbContext.Species
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);   

        if (species == null)
            return Errors.General.NotFound(id.Value);

        return species;
    }

    public async Task<Result<Breed, Error>> GetBreedAsync(BreedId id, CancellationToken cancellationToken)
    {
        var breed = await _dbContext.Breeds
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

        if (breed == null)

            return Errors.General.NotFound(id.Value);

        return breed;
    }
}
