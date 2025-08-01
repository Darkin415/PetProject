using PetProject.Contracts.Dtos;
using PetProject.Species.Contracts.DTOs;
using PetProject.Volunteers.Contracts.DTOs;

namespace PetProject.Species.Application;

public interface ISpeciesReadDbContext
{
    public IQueryable<BreedDto> Breeds { get; }

    public IQueryable<SpeciesDto> Species { get; }
}