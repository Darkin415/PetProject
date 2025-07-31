namespace PetProject.Species.Application.GetSpecies;

public class GetSpeciesWithPaginationHandler : IQueryHandler<PagedList<SpeciesDto>, GetSpeciesWithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetSpeciesWithPaginationHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }


    public async Task<PagedList<SpeciesDto>> Handle(GetSpeciesWithPaginationQuery query,
        CancellationToken cancellationToken)
    {
        var speciesQuery = _readDbContext.Species;
        
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