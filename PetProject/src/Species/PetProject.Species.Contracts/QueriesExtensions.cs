
using Microsoft.EntityFrameworkCore;
using PetProject.Species.Contracts.Models;

namespace PetProject.Species.Contracts;

public static class QueriesExtensions
{
    public static async Task<PagedList<T>> ToPagedList<T>(
        this IQueryable<T> source, 
        int page, 
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var totalcount = await source.CountAsync(cancellationToken);

        var items = await source.Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedList<T>
        {
            Items = items,
            PageSize = pageSize,
            Page = page,
            TotalCount = totalcount
        };
    }
} 