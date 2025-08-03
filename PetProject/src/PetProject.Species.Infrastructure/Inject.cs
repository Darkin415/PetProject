using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetProject.Core.Abstraction;
using PetProject.Core.Database;
using PetProject.Species.Application.Breed;
using PetProject.Species.Application.DeleteBreed;
using PetProject.Species.Application.DeleteSpecies;
using PetProject.Species.Application.Species;
using PetProject.Species.Contracts;
using PetProject.Species.Infrastructure.DbContexts;
using PetProject.Species.Infrastructure.Repository;
using PetProject.Volunteers.Contracts;

namespace PetProject.Species.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddSpeciesInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<WriteSpeciesDbContext>();

        services.AddScoped<CreateSpeciesHandler>();
        
        services.AddScoped<CreateBreedHandler>();
        
        // services.AddScoped<DeleteBreedHandler>();
        
        // services.AddScoped<DeleteSpeciesHandler>();
        
        
        services.AddScoped<ISpeciesContract, SpeciesRepository>();

        return services;
    }
    
    public static IServiceCollection AddSpeciesApplication(this IServiceCollection services)
    {
        return services
            .AddSpeciesCommands()
            .AddSpeciesQueries()
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    private static IServiceCollection AddSpeciesCommands(this IServiceCollection services)
    {
        return services.Scan(scan => scan
            .FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses(classes => classes.AssignableToAny(
                typeof(ICommandHandler<,>),
                typeof(ICommandHandler<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());
    }
    
    private static IServiceCollection AddSpeciesQueries(this IServiceCollection services)
    {
        return services.Scan(scan => scan
            .FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());
    }
    
   
}