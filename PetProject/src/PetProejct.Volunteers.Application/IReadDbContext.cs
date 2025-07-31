using PetProject.Contracts.Dtos;

namespace PetProejct.Volunteers.Application;

public interface IReadDbContext
{
    IQueryable<VolunteerDto> Volunteers { get; }
    IQueryable<PetsDto> Pets { get; }
    IQueryable<BreedDto> Breed { get; }
    
    IQueryable<SpeciesDto> Species { get; }
}
