using Microsoft.EntityFrameworkCore;
using PetProject.Contracts.Dtos;

namespace PetProject.Infrastructure.DbContexts;

public interface IReadDbContext
{
    IQueryable<VolunteerDto> Volunteers { get; }
    IQueryable<PetDto> Pets { get; }
}
