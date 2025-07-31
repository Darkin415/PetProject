using PetProejct.Volunteers.Application.Volunteers.Queries.GetVolunteerWithPagination;
using PetProject.Application.Extensions;
using PetProject.Application.Models;
using PetProject.Contracts.Abstraction;
using PetProject.Contracts.Dtos;

namespace PetProejct.Volunteers.Application.Volunteers.GetVolunteers;
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
