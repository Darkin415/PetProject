using PetProject.Contracts.Dtos;
using PetProject.Core.Abstraction;
using PetProject.Species.Contracts;
using PetProject.Species.Contracts.DTOs;
using PetProject.Species.Contracts.Models;


namespace PetProject.Species.Application.GetSpecies;

public class GetSpeciesWithPaginationHandler : IQueryHandler<PagedList<SpeciesDto>, GetSpeciesWithPaginationQuery>
{
    private readonly ISpeciesReadDbContext _speciesReadDbContext;

    public GetSpeciesWithPaginationHandler(ISpeciesReadDbContext speciesReadDbContext)
    {
        _speciesReadDbContext = speciesReadDbContext;
    }


    public async Task<PagedList<SpeciesDto>> Handle(GetSpeciesWithPaginationQuery query,
        CancellationToken cancellationToken)
    {
        var speciesQuery = _speciesReadDbContext.Species;
        
        var pagedList = await speciesQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);

        return pagedList;
    }
}

public record GetSpeciesWithPaginationQuery(
    int Page, int PageSize) : IQuery;

public record GetSpeciesWithPaginationRequest(int Page, int PageSize)
{
    public GetSpeciesWithPaginationQuery ToQuery() =>
        new(Page, PageSize);
}