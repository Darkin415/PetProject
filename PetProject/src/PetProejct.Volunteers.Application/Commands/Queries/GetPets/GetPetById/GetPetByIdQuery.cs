
using PetProject.Core.Abstraction;

namespace PetProejct.Volunteers.Application.Commands.Queries.GetPets.GetPetById;

public record GetPetByIdQuery(Guid Id) : IQuery;