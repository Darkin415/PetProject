using PetProject.Contracts.Abstraction;

namespace PetProejct.Volunteers.Application.Pet.GetPets;

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

