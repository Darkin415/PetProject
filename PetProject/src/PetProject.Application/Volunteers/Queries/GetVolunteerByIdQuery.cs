using PetProject.Application.Abstraction;

namespace PetProject.Application.Volunteers.Queries;

public record GetVolunteerByIdQuery(Guid Id) : IQuery;
