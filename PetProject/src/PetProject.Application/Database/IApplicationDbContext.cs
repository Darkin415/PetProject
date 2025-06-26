using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using PetProject.Domain.Volunteers;
using System.Data;

namespace PetProject.Application.Database;

public interface IApplicationDbContext
{
    DbSet<Volunteer> Volunteers { get; }

    DatabaseFacade Database { get; }

    Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

