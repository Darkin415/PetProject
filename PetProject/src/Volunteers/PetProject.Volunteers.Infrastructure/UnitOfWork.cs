using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using PetProject.Core.Database;
using PetProject.Volunteers.Infrastructure.DbContexts;

namespace PetProject.Volunteers.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly VolunteerWriteDbContext _dbContext;

    public UnitOfWork(VolunteerWriteDbContext dbContext)
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
