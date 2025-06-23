using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetProject.Application.Volunteers;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObject;
using PetProject.Domain.Volunteers;

namespace PetProject.Infrastructure.Repositories;

public class VolunteersRepository : IVolunteersRepository
{
    private readonly ApplicationDbContext _dbContext;
    private List<Pet> _pets = [];
    public VolunteersRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);

        return volunteer.Id;
    }

    public Guid Save(Volunteer volunteer, CancellationToken cancellationToken)
    {
        _dbContext.Volunteers.Attach(volunteer);
     
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

    public Guid Delete(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        _dbContext.Volunteers.Remove(volunteer);
        
        return volunteer.Id;
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
