using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetProject.Volunteers.Domain;
using PetProject.Volunteers.Domain.Entities;

namespace PetProject.Volunteers.Infrastructure.DbContexts;

public class VolunteerWriteDbContext(IConfiguration configuration) 
    : DbContext
{   
    
    public DbSet<Volunteer> Volunteers => Set<Volunteer>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("Database"));        
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        optionsBuilder.EnableSensitiveDataLogging();
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(VolunteerWriteDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Write") ?? false);
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
