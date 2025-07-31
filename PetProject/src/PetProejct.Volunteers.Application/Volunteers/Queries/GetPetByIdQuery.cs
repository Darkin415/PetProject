using PetProject.Contracts.Abstraction;

namespace PetProejct.Volunteers.Application.Volunteers.Queries;

public record GetPetByIdQuery(Guid Id) : IQuery;