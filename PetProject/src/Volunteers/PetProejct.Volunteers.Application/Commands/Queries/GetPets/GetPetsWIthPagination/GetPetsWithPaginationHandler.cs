using PetProject.Core.Abstraction;
using PetProject.Species.Contracts;
using PetProject.Species.Contracts.Models;
using PetProject.Volunteers.Contracts.DTOs;

namespace PetProejct.Volunteers.Application.Commands.Queries.GetPets.GetPetsWIthPagination;

public class GetPetsWithPaginationHandler : IQueryHandler<PagedList<PetsDto>, GetPetsWithPaginationQuery>
{
    private readonly IVolunteersReadDbContext _volunteersReadDbContext;

    public GetPetsWithPaginationHandler(IVolunteersReadDbContext volunteersReadDbContext)
    {
        _volunteersReadDbContext = volunteersReadDbContext;
    }
    public async Task<PagedList<PetsDto>> Handle(GetPetsWithPaginationQuery query, CancellationToken cancellationToken)
    {
        var petsQuery = _volunteersReadDbContext.Pets;
        
        if (!string.IsNullOrEmpty(query.NickName))
            petsQuery = petsQuery.Where(p => p.Nickname.Contains(query.NickName));

        if (query.Weight.HasValue)
            petsQuery = petsQuery.Where(p => p.Weight == query.Weight);
        
        if (!string.IsNullOrEmpty(query.Color))
            petsQuery = petsQuery.Where(p => p.Color.Contains(query.Color));      
        
        if (!string.IsNullOrEmpty(query.CastrationStatus))
            petsQuery = petsQuery.Where(p => p.CastrationStatus.Contains(query.CastrationStatus));      
        
        if (!string.IsNullOrEmpty(query.VaccinationStatus))
            petsQuery = petsQuery.Where(p => p.VaccinationStatus.Contains(query.VaccinationStatus));
        
        var pagedList = await petsQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);

        return pagedList;
    }
}

