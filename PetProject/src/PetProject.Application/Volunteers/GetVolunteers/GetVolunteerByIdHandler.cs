using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetProject.Application.Abstraction;
using PetProject.Application.Database;
using PetProject.Application.Models;
using PetProject.Application.Volunteers.Queries;
using PetProject.Application.Volunteers.Queries.GetVolunteerWithPagination;
using PetProject.Contracts.Dtos;
using PetProject.Domain.Shared.ValueObjects;

namespace PetProject.Application.Volunteers.GetVolunteers;

public class GetVolunteerByIdHandler : IQueryHandler<Result<VolunteerDto, Error>, GetVolunteerByIdQuery>
{
    public readonly IReadDbContext _readDbContext;

    public GetVolunteerByIdHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<VolunteerDto, Error>> Handle(GetVolunteerByIdQuery query, CancellationToken cancellationToken)
    {
        var volunteerDto = await _readDbContext.Volunteers
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
