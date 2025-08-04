using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using PetProject.Core.Database;
using PetProject.Species.Infrastructure.DbContexts;

namespace PetProject.Species.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly WriteSpeciesDbContext _dbContext;

    public UnitOfWork(WriteSpeciesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IDbTransaction> BeginTransaction(CancellationToken cancellationToken = default)
    {
        var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        return transaction.GetDbTransaction();
    }

    public async Task SaveChanges(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
        
    }
}
