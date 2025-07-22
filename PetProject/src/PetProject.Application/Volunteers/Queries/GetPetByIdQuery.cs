using PetProject.Application.Abstraction;

namespace PetProject.Application.Volunteers.Queries;

public record GetPetByIdQuery(Guid Id) : IQuery;