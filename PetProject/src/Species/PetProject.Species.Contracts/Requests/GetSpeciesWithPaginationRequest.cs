using PetProject.Core.Abstraction;

namespace PetProject.Species.Contracts.Requests;

public record GetSpeciesWithPaginationQuery(
    int Page, int PageSize) : IQuery;

public record GetSpeciesWithPaginationRequest(int Page, int PageSize)
{
    public GetSpeciesWithPaginationQuery ToQuery() =>
        new(Page, PageSize);
}