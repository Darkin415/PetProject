using PetProject.Species.Contracts.DTOs;


namespace PetProject.Species.Application;

public interface ISpeciesReadDbContext
{
    public IQueryable<BreedDto> Breeds { get; }

    public IQueryable<SpeciesDto> Species { get; }
}