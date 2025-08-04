using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetProject.SharedKernel;
using PetProject.SharedKernel.ValueObjects;
using PetProject.Species.Application;
using PetProject.Species.Application.Breed;
using PetProject.Species.Application.GetBreedBySpeciesId;
using PetProject.Species.Application.GetSpecies;
using PetProject.Species.Application.Species;
using PetProject.Species.Contracts;
using PetProject.Species.Contracts.DTOs;
using PetProject.Species.Contracts.Models;
using PetProject.Species.Contracts.Requests;
using PetProject.Species.Domain.PetSpecies;

namespace PetProject.Species.Presentation;

public class SpeciesContract(
    ISpeciesReadDbContext readDbContext, 
    CreateSpeciesHandler createSpeciesHandler, 
    GetBreedBySpeciesIdHandler getBreedBySpeciesIdHandler,
    GetSpeciesWithPaginationHandler getSpeciesWithPaginationHandler)
    : ISpeciesContract
{
    public async Task<Result<BreedDto, Error>> GetBreedByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var breed = await readDbContext.Breeds.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (breed == null)
            return Error.NotFound("Breed.not.found", "Breed not found");

        return breed;
    }

    public async Task<Result<Guid, ErrorList>> AddSpecies(CreateSpeciesRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateSpeciesCommand(request.Title);
        
        return await createSpeciesHandler.Handle(command, cancellationToken);
    }

    public async Task<Result<SpeciesDto, Error>> GetSpeciesByNameAsync(string title, CancellationToken cancellationToken)
    {
        var species = await readDbContext.Species.FirstOrDefaultAsync(p => p.title == title, cancellationToken);
        if(species == null)
            return Error.NotFound("Species.not.found", "Species not found");

        return species;
    }

    public async Task<Result<BreedDto, Error>> GetBreedByNameAsync(string name, CancellationToken cancellationToken)
    {
        var breed = await readDbContext.Breeds.FirstOrDefaultAsync(p => p.Name == name, cancellationToken);
        if(breed == null)
            return Error.NotFound("Breed.not.found", "Breed not found");

        return breed;
    }

    public async Task<Result<List<BreedDto>, ErrorList>> GetBreedsBySpeciesId(Guid speciesId, CancellationToken cancellationToken)
    {
        var command = new GetBreedBySpeciesIdCommand(speciesId);
    
        var result = await getBreedBySpeciesIdHandler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error; 
    
        var dtos = result.Value.Select(breed => new BreedDto
        {
            Id = breed.Id.Value,
            Name = breed.Title.Name 
        }).ToList();

        return Result.Success<List<BreedDto>, ErrorList>(dtos); 
    }

    public async Task<PagedList<SpeciesDto>> GetSpeciesWithPaginationAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        var query  = new GetSpeciesWithPaginationQuery(page, pageSize);
        
        return await getSpeciesWithPaginationHandler.Handle(query, cancellationToken);
    }
}