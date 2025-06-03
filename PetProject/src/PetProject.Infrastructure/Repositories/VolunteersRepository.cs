using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetProject.Application.Volunteers;
using PetProject.Domain;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObject;
using PetProject.Domain.Volunteers;

namespace PetProject.Infrastructure.Repositories;

public class VolunteersRepository : IVolunteersRepository
{
    private readonly ApplicationDbContext _dbContext;
    public VolunteersRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return volunteer.Id;
    }

    public async Task<Guid> Save(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        _dbContext.Volunteers.Attach(volunteer);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return volunteer.Id;
    }


    public async Task<Result<Volunteer, Error>> GetById(VolunteerId volunteerId, CancellationToken cancellationToken = default)
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

}
