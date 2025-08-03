using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetProejct.Volunteers.Application.Commands.Queries.GetVolunteers;

using PetProject.Core.Abstraction;
using PetProject.SharedKernel;
using PetProject.Volunteers.Contracts.DTOs;

namespace PetProejct.Volunteers.Application.Commands.GetVolunteerById;

public class GetVolunteerByIdHandler : IQueryHandler<Result<VolunteerDto, Error>, GetVolunteerByIdQuery>
{
    public readonly IVolunteersReadDbContext VolunteersReadDbContext;

    public GetVolunteerByIdHandler(IVolunteersReadDbContext volunteersReadDbContext)
    {
        VolunteersReadDbContext = volunteersReadDbContext;
    }

    public async Task<Result<VolunteerDto, Error>> Handle(GetVolunteerByIdQuery query, CancellationToken cancellationToken)
    {
        var volunteerDto = await VolunteersReadDbContext.Volunteers
            .AsNoTracking()
            .Where(v => v.Id == query.Id)
            .Select(v => new VolunteerDto
            {
                Id = v.Id,
                Description = v.Description,
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (volunteerDto == null)
            return Error.NotFound("volunteer_not_found", "Volunteer not found");

        return volunteerDto;
    }
}
