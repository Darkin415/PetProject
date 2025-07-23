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
using PetProject.Application.Database;

namespace PetProject.Infrastructure.Repositories;

public class SpeciesRepository : ISpeciesRepository
{
    private readonly WriteDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;

    public SpeciesRepository(WriteDbContext dbContext, IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> CreateSpecies(string title, CancellationToken cancellationToken)
    {
        var name = Title.Create(title);
        if (name.IsFailure)
            return name.Error.ToErrorList();
        
        var speciesId = SpeciesId.NewSpeciesId();

        if (await _dbContext.Species.AnyAsync(s => s.Title.Name == title, cancellationToken))
            return Error.Conflict("species.already.exists", "Species already exists").ToErrorList();
        
        var species = new Species(speciesId, name.Value);

        await _dbContext.Species.AddAsync(species, cancellationToken);

        await _unitOfWork.SaveChanges(cancellationToken);

        return species.Id.Value;

    }
    
    // public async Task<Result<Guid, ErrorList>> CreateBreed(string title, CancellationToken cancellationToken)
    // {
    //     var name = Title.Create(title);
    //     if (name.IsFailure)
    //         return name.Error.ToErrorList();
    //
    //     var breedId = BreedId.NewBreedId();
    //     
    //     if (await _dbContext.Species.AnyAsync(s => s.Title.Name == title, cancellationToken))
    //         return Error.Conflict("breed.already.exists", "Breed already exists").ToErrorList();
    //     
    //     var breed = new Breed(breedId, name.Value);
    //
    //     await _dbContext.Breeds.AddAsync(breed, cancellationToken);
    //
    //     await _unitOfWork.SaveChanges(cancellationToken);
    //
    //     return breed.Id.Value;
    //
    // }

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
