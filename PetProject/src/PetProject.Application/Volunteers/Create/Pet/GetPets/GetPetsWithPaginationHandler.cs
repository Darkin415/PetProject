using PetProject.Application.Abstraction;
using PetProject.Application.Extensions;
using PetProject.Application.Models;
using PetProject.Contracts.Dtos;
using PetProject.Infrastructure.DbContexts;

namespace PetProject.Application.Volunteers.Create.Pet.GetPets;

public class GetPetsWithPaginationHandler : IQueryHandler<PagedList<PetsDto>, GetPetsWithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetPetsWithPaginationHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    public async Task<PagedList<PetsDto>> Handle(GetPetsWithPaginationQuery query, CancellationToken cancellationToken)
    {
        var petsQuery = _readDbContext.Pets;
        
        if (!string.IsNullOrEmpty(query.NickName))
            petsQuery = petsQuery.Where(p => p.Nickname.Contains(query.NickName));
        
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

