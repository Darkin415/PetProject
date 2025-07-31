using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetProejct.Volunteers.Application;
using PetProject.Contracts;
using PetProject.Contracts.Ids;
using PetProject.Contracts.ValueObjects;
using PetProject.Volunteers.Domain;
using PetProject.Volunteers.Domain.Pets;
using PetProject.Volunteers.Infrastructure.DbContexts;

namespace PetProject.Volunteers.Infrastructure.Repositories;

public class VolunteersRepository : IVolunteersRepository
{
    private readonly WriteDbContext _dbContext;
    private List<Pet> _pets = [];
    public VolunteersRepository(WriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }    

    public async Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);
        return volunteer.Id.Value;
    }

    public Guid Save(Volunteer volunteer, CancellationToken cancellationToken)
    {
        _dbContext.Volunteers.Attach(volunteer);
        return volunteer.Id.Value;
    }

    public Guid Delete(Volunteer volunteer, CancellationToken cancellationToken)
    {
        _dbContext.Volunteers.Remove(volunteer);
        return volunteer.Id.Value;
    } 

    public async Task<Result<Volunteer, Error>> GetVolunteerById(VolunteerId volunteerId, CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefaultAsync(v => v.Id == volunteerId);

        if (volunteer == null)

            return Errors.General.NotFound(volunteerId);

        return volunteer;
    }

    
    public async Task<Result<Volunteer, Error>> GetByEmail(Email email, CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Volunteers
            .FirstOrDefaultAsync(v => v.Email == email);

        if (volunteer == null)

            return Errors.General.NotFound();

        return volunteer;

    }
    public Result<Pet, Error> GetByPetId(PetId id)
    {
        var volunteer = _dbContext.Volunteers
        .Include(v => v.Pets) 
        .FirstOrDefault(v => v.Pets.Any(p => p.Id == id));

        if (volunteer == null)
            return Errors.General.NotFound();

        var pet = volunteer.Pets.First(p => p.Id == id);
        return pet;       
    }    


}
