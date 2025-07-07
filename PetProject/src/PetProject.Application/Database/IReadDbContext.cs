using Microsoft.EntityFrameworkCore;
using PetProject.Contracts.Dtos;

namespace PetProject.Infrastructure.DbContexts;

public interface IReadDbContext
{
    DbSet<VolunteerDto> Volunteers { get; }
    DbSet<PetDto> Pets { get; }
}
