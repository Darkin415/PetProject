using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetProject.Application.Abstraction;
using PetProject.Application.Volunteers.Create.Pet.AddPet;
using PetProject.Application.Volunteers.Create.Pet.AddPetPhoto;
using PetProject.Application.Volunteers.Create.SocialList;
using PetProject.Application.Volunteers.CreateVolunteer;
using PetProject.Application.Volunteers.Delete;
using PetProject.Application.Volunteers.DeletePhotos;
using PetProject.Application.Volunteers.GetVolunteers;
using PetProject.Application.Volunteers.UpdateMainInfo;
using PetProject.Contracts.Commands;

namespace PetProject.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddCommands()
            .AddQueries()
            .AddValidatorsFromAssembly(typeof(Inject).Assembly);
        
        return services;

    }

    private static IServiceCollection AddCommands(this IServiceCollection services)
    {
       return services.Scan(scan => scan.FromAssemblies(typeof(Inject).Assembly)
        .AddClasses(classes => classes.AssignableToAny(typeof(ICommandHandler<,>), typeof(ICommandHandler<>)))
        .AsSelfWithInterfaces()
        .WithScopedLifetime());
        
    }

    private static IServiceCollection AddQueries(this IServiceCollection services)
    {
        return services.Scan(scan => scan.FromAssemblies(typeof(Inject).Assembly)
         .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
         .AsSelfWithInterfaces()
         .WithScopedLifetime());
    }
}
