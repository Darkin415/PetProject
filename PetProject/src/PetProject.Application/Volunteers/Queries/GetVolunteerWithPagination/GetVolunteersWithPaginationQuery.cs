using PetProject.Application.Abstraction;

namespace PetProject.Application.Volunteers.Queries.GetVolunteerWithPagination;

public record GetVolunteersWithPaginationQuery(int Page, int PageSize) : IQuery;
