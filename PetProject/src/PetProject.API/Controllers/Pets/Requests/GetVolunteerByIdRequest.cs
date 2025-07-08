using PetProject.Application.Volunteers.Queries;
using PetProject.Application.Volunteers.Queries.GetVolunteerWithPagination;

namespace PetProject.API.Controllers.Pets.Requests;

public record GetVolunteerByIdRequest()
{
    public GetVolunteerByIdQuery ToQuery(Guid id) =>
        new(id);
}


