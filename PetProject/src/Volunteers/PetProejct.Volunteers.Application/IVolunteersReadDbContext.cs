
using PetProject.Volunteers.Contracts.DTOs;

namespace PetProejct.Volunteers.Application;

public interface IVolunteersReadDbContext
{
    IQueryable<VolunteerDto> Volunteers { get; }
    IQueryable<PetsDto> Pets { get; }
    
}