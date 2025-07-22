using PetProject.Application.Volunteers.Create.Pet.GetPets;
using PetProject.Application.Volunteers.Queries.GetVolunteerWithPagination;

namespace PetProject.API.Controllers.Pets.Requests;

public record GetVolunteerWithPaginationRequest(int Page, int PageSize)
{
    public GetVolunteersWithPaginationQuery ToQuery() =>
        new(Page, PageSize);
}

public record GetPetsWithPaginationRequest(
    int Page, 
    int PageSize,
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
            NickName,
            Color,            
            CastrationStatus, 
            VaccinationStatus);         
}


