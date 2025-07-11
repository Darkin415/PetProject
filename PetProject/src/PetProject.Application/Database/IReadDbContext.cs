using Microsoft.EntityFrameworkCore;
using PetProject.Application.Volunteers.Create.Pet.GetPets;
using PetProject.Contracts.Dtos;

namespace PetProject.Infrastructure.DbContexts;

public interface IReadDbContext
{
    IQueryable<VolunteerDto> Volunteers { get; }
    IQueryable<PetsDto> Pets { get; }
}
