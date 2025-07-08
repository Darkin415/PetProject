using Microsoft.EntityFrameworkCore;
using PetProject.Application.Volunteers.Queries;
using PetProject.Contracts.Dtos;
using PetProject.Infrastructure.DbContexts;

namespace PetProject.Application.Volunteers.GetVolunteers;

public class GetVolunteerByIdHandler
{
    public readonly IReadDbContext _readDbContext;

    public GetVolunteerByIdHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    public async Task<VolunteerDto> Handle(GetVolunteerByIdQuery query, CancellationToken cancellationToken)
    {
        var volunteerResult = await _readDbContext.Volunteers
            .AsNoTracking()
            .Where(v => v.Id == query.Id)
            .Select(v => new VolunteerDto
            {
                Id = v.Id,
                Description = v.Description,
            })
            .FirstOrDefaultAsync(cancellationToken);

        if(volunteerResult == null)
            throw new ArgumentNullException(nameof(volunteerResult));  

        return volunteerResult;      
    }
}
