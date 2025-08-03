using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using PetProject.Core.Constants;
using PetProject.Species.Domain.PetSpecies;

namespace PetProject.Species.Infrastructure.DbContexts;

public class WriteSpeciesDbContext(IConfiguration configuration) : DbContext 
{
    public DbSet<Domain.PetSpecies.Species> Species => Set<Domain.PetSpecies.Species>();
    public DbSet<Breed> Breeds => Set<Breed>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(Constants.DATABASE));        
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        optionsBuilder.EnableSensitiveDataLogging();
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.ApplyConfigurationsFromAssembly(
        //     typeof(WriteSpeciesDbContext).Assembly,
        //     type => type.FullName?.Contains("Configurations.Write") ?? false);
        
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new SpeciesConfiguration());
        modelBuilder.ApplyConfiguration(new BreedConfiguration());
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