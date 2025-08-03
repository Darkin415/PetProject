using PetProejct.Volunteers.Application.Commands.Queries.GetPets.GetPetsWIthPagination;
using PetProejct.Volunteers.Application.Commands.Queries.GetVolunteers;

namespace PetProject.Volunteers.Presentation.Pets.Requests;

public record GetVolunteerWithPaginationRequest(int Page, int PageSize)
{
    public GetVolunteersWithPaginationQuery ToQuery() =>
        new(Page, PageSize);
}

public record GetPetsWithPaginationRequest(
    int Page, 
    int PageSize,
    double? Weight,
    double? Height,
    string? NickName,
    string? Color,     
    string? CastrationStatus,
    string? VaccinationStatus
    )
{
    public GetPetsWithPaginationQuery ToQuery(Guid VolunteerId) =>
        new(VolunteerId, 
            Page, 
            PageSize, 
            Weight,
            Height,
            NickName,
            Color,            
            CastrationStatus, 
            VaccinationStatus);         
}


