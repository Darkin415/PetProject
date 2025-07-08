using PetProject.Application.Volunteers.Queries.GetVolunteerWithPagination;

namespace PetProject.API.Controllers.Pets.Requests;

public record GetVolunteerWithPaginationRequest(int Page, int PageSize)
{
    public GetVolunteersWithPaginationQuery ToQuery() =>
        new(Page, PageSize);
}


