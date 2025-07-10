using Microsoft.EntityFrameworkCore;
using PetProject.Application.Abstraction;
using PetProject.Application.Extensions;
using PetProject.Application.Models;
using PetProject.Application.Volunteers.Queries.GetVolunteerWithPagination;
using PetProject.Contracts.Dtos;
using PetProject.Infrastructure.DbContexts;

namespace PetProject.Application.Volunteers.GetVolunteers;
public class GetVolunteersWithPaginationHandler : IQueryHandler<PagedList<VolunteerDto>, GetVolunteersWithPaginationQuery>
{
    public readonly IReadDbContext _readDbContext;

    public GetVolunteersWithPaginationHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    public async Task<PagedList<VolunteerDto>> Handle(GetVolunteersWithPaginationQuery query, CancellationToken cancellationToken)
    {
        var volunteerQuery = _readDbContext.Volunteers;

        var pagedList = await volunteerQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);

        return pagedList;      
    }
}
