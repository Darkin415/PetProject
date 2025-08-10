using Microsoft.Extensions.DependencyInjection;
using PetProject.Species.Application.Breed;
using PetProject.Species.Application.DeleteSpecies;
using PetProject.Species.Application.GetBreedBySpeciesId;
using PetProject.Species.Application.GetSpecies;
using PetProject.Species.Application.Species;

namespace PetProject.Species.Application;

public static class Inject
{
    public static IServiceCollection AddSpeciesUseCases(this IServiceCollection services)
    {
        services.AddScoped<CreateSpeciesHandler>();
        
        services.AddScoped<CreateBreedHandler>();
        
        services.AddScoped<GetBreedBySpeciesIdHandler>();

        services.AddScoped<GetSpeciesWithPaginationHandler>();
        
        // services.AddScoped<DeleteBreedHandler>();
        
        services.AddScoped<DeleteSpeciesHandler>();
        
        return services;
    }
}