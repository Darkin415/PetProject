using PetProject.Contracts.Abstraction;

namespace PetProejct.Volunteers.Application.Volunteers.Queries.GetVolunteerWithPagination;

public record GetVolunteersWithPaginationQuery(int Page, int PageSize) : IQuery;
