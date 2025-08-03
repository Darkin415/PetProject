using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using PetProject.Core.Database;
using PetProject.Core.Enum;
using PetProject.SharedKernel;
using PetProject.SharedKernel.ValueObjects;
using PetProject.Species.Application;
using PetProject.Species.Contracts;
using PetProject.Species.Domain.PetSpecies;
using PetProject.Species.Infrastructure.DbContexts;

namespace PetProject.Species.Infrastructure.Repository;

public class SpeciesRepository : ISpeciesRepository
{
    private readonly WriteSpeciesDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;

    public SpeciesRepository(WriteSpeciesDbContext dbContext, 
        [FromKeyedServices(ModuleKey.Species)] IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
    }
    

    public async Task<Result<Domain.PetSpecies.Species, Error>> GetSpeciesByNameAsync(Title title, CancellationToken cancellationToken)
    {
        var species = await _dbContext.Species
            .FirstOrDefaultAsync(s => s.Title.Name == title.Name, cancellationToken);   

        if (species == null)
            return Errors.General.NotFound();

        return species;
    }

    public async Task<Result<List<Breed>, Error>> GetBreedsBySpeciesId(SpeciesId speciesId, CancellationToken cancellationToken)
    {
        var breeds = await _dbContext.Breeds
            .Where(b => b.SpeciesId == speciesId)
            .ToListAsync(cancellationToken);

        return breeds;

    }
    
    public async Task<Result<Breed, Error>> GetBreedByNameAsync(Title title, CancellationToken cancellationToken)
    {
        var breed = await _dbContext.Breeds
            .FirstOrDefaultAsync(s => s.Title.Name == title.Name, cancellationToken);   

        if (breed == null)
            return Errors.General.NotFound();

        return breed;
    }

    public async Task<Result<Domain.PetSpecies.Species, Error>> GetSpeciesByIdAsync(SpeciesId id, CancellationToken cancellationToken)
    {
        var species = await _dbContext.Species
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);   

        if (species == null)
            return Errors.General.NotFound(id.Value);

        return species;
    }
    
    public async Task<Result<Guid, ErrorList>> AddSpecies(Domain.PetSpecies.Species species, CancellationToken cancellationToken)
    {
       
        if (await _dbContext.Species.AnyAsync(s => s.Title.Name == species.Title.Name, cancellationToken))
            return Error.Conflict("species.already.exists", "Species already exists").ToErrorList();
        
        await _dbContext.Species.AddAsync(species, cancellationToken);

        await _unitOfWork.SaveChanges(cancellationToken);

        return species.Id.Value;

    }
    
    public async Task<Result<Guid, ErrorList>> AddBreed(Breed breed, CancellationToken cancellationToken)
    {
        
        if (await _dbContext.Breeds.AnyAsync(s => s.Title.Name == breed.Title.Name, cancellationToken))
            return Error.Conflict("breed.already.exists", "Breed already exists").ToErrorList();
        
        await _dbContext.Breeds.AddAsync(breed, cancellationToken);

        await _unitOfWork.SaveChanges(cancellationToken);

        return breed.Id.Value;

    }
    
    public Guid DeleteBreed(Breed breed, CancellationToken cancellationToken)
    {
        _dbContext.Breeds.Remove(breed);
        return breed.Id.Value;
    } 
    
    public Guid DeleteSpecies(Domain.PetSpecies.Species species, CancellationToken cancellationToken)
    {
        _dbContext.Species.Remove(species);
        return species.Id.Value;
    } 

    public async Task<Result<Breed, Error>> GetBreedByIdAsync(BreedId id, CancellationToken cancellationToken)
    {
        var breed = await _dbContext.Breeds
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

        if (breed == null)

            return Errors.General.NotFound(id.Value);

        return breed;
    }
}
