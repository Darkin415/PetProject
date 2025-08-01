using PetProject.Volunteers.Contracts.DTOs;

namespace PetProject.Volunteers.Contracts;

public interface IReadVolunteersDbContext
{
    public IQueryable<VolunteerDto> Volunteers { get; }

    public IQueryable<PetsDto> Pets { get; }
}