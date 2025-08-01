

using PetProject.Core.Abstraction;

namespace PetProejct.Volunteers.Application.Commands.Queries.GetVolunteers;

public record GetVolunteersWithPaginationQuery(int Page, int PageSize) : IQuery;
