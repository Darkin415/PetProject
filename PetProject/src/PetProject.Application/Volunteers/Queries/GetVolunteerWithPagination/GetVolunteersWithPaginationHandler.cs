using Microsoft.EntityFrameworkCore;
using PetProject.Application.Extensions;
using PetProject.Application.Models;
using PetProject.Contracts.Dtos;
using PetProject.Infrastructure.DbContexts;

namespace PetProject.Application.Volunteers.Queries.GetVolunteerWithPagination;
public class GetVolunteersWithPaginationHandler
{
    public readonly IReadDbContext _readDbContext;

    public GetVolunteersWithPaginationHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    public async Task<PagedList<VolunteerDto>> Handle(GetVolunteersWithPaginationQuery query, CancellationToken cancellationToken)
    {
        var volunteerQuery = _readDbContext.Volunteers.AsNoTracking().AsQueryable();

        var pagedList = await volunteerQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);

        return pagedList;      
    }
}
