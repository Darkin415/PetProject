using PetProject.Core.Abstraction;

namespace PetProejct.Volunteers.Application.Commands.Queries.GetVolunteers;

public record GetVolunteerByIdQuery(Guid Id) : IQuery;