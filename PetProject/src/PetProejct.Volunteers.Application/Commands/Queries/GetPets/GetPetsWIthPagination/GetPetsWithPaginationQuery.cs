using PetProject.Core.Abstraction;

namespace PetProejct.Volunteers.Application.Commands.Queries.GetPets.GetPetsWIthPagination;

public record GetPetsWithPaginationQuery(
    Guid? VolunteerId,
    int Page, 
    int PageSize,
    double? Weight,
    double? Height,
    string? NickName,
    string? Color,      
    string? CastrationStatus,
    string? VaccinationStatus      
    ) : IQuery;

