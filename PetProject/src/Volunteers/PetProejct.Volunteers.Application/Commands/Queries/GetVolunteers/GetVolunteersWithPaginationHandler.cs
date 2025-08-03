
using PetProject.Core.Abstraction;
using PetProject.Species.Contracts;
using PetProject.Species.Contracts.Models;
using PetProject.Volunteers.Contracts.DTOs;

namespace PetProejct.Volunteers.Application.Commands.Queries.GetVolunteers;
public class GetVolunteersWithPaginationHandler : IQueryHandler<PagedList<VolunteerDto>, GetVolunteersWithPaginationQuery>
{
    public readonly IVolunteersReadDbContext VolunteersReadDbContext;

    public GetVolunteersWithPaginationHandler(IVolunteersReadDbContext volunteersReadDbContext)
    {
        VolunteersReadDbContext = volunteersReadDbContext;
    }
    public async Task<PagedList<VolunteerDto>> Handle(GetVolunteersWithPaginationQuery query, CancellationToken cancellationToken)
    {
        var volunteerQuery = VolunteersReadDbContext.Volunteers;

        var pagedList = await volunteerQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);

        return pagedList;      
    }
}
