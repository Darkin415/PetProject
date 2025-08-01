using Microsoft.EntityFrameworkCore;
using PetProject.Contracts.Dtos;
using PetProject.Species.Application;
using PetProject.Species.Contracts.DTOs;
using PetProject.Volunteers.Contracts.DTOs;

namespace PetProject.Species.Infrastructure.DbContexts;

public class SpeciesReadDbContext : DbContext, ISpeciesReadDbContext
{
    
    public IQueryable<SpeciesDto> Species => Set<SpeciesDto>();
    
    public IQueryable<BreedDto> Breeds => Set<BreedDto>();
    
    public IQueryable<VolunteerDto> Volunteers { get; }
    
    public IQueryable<PetsDto> Pets { get; }
}