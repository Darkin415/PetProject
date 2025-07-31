using PetProject.Contracts.Abstraction;

namespace PetProejct.Volunteers.Application.Volunteers.Queries;

public record GetVolunteerByIdQuery(Guid Id) : IQuery;