using PetProejct.Volunteers.Application;
using PetProject.Files.Infrastructure;
using PetProject.Species.Application;
using PetProject.Species.Infrastructure;
using PetProject.Volunteers.Infrastructure;

namespace PetProject.API;

public static class DependencyInjection
{
    public static IServiceCollection AddVolunteerModule(
        this IServiceCollection services)
    {
        services.AddVolunteersInfrastructure();
        services.AddVolunteersUseCases();

        return services;
    }

    public static IServiceCollection AddSpeciesModule(
        this IServiceCollection services)
    {
        services.AddSpeciesInfrastructure();
        services.AddSpeciesUseCases();

        return services;
    }

    public static IServiceCollection AddFilesModule(
        this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddFilesInfrastructure(configuration);
        
        return services;
    }
}