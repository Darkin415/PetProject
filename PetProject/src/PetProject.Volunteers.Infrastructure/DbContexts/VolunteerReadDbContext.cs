using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetProejct.Volunteers.Application;
using PetProject.Contracts.Dtos;
using PetProject.Volunteers.Contracts.DTOs;

namespace PetProject.Volunteers.Infrastructure.DbContexts;
public class VolunteerReadDbContext(IConfiguration configuration)
    : DbContext, IVolunteersReadDbContext
{
    
    public IQueryable<VolunteerDto> Volunteers => Set<VolunteerDto>();

    public IQueryable<PetsDto> Pets => Set<PetsDto>();
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("Database"));
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(VolunteerWriteDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);
    }
    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });

    public async Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken = default)
    {
        return await base.Database.BeginTransactionAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        return base.SaveChangesAsync(cancellationToken);
    }
}