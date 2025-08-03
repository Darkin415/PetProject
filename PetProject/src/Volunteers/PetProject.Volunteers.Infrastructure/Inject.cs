using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetProejct.Volunteers.Application;
using PetProejct.Volunteers.Application.Commands.CreateVolunteer;
using PetProejct.Volunteers.Application.Commands.DeleteVolunteer;
using PetProejct.Volunteers.Application.Commands.GetVolunteerById;
using PetProejct.Volunteers.Application.Commands.UpdateVolunteerMainInfo;
using PetProject.Core.Abstraction;
using PetProject.Core.Database;
using PetProject.Volunteers.Infrastructure.DbContexts;
using PetProject.Volunteers.Infrastructure.Repositories;

namespace PetProject.Volunteers.Infrastructure;

public static class Inject
{
    
    public static IServiceCollection AddVolunteersApplication(this IServiceCollection services)
    {
        return services
            .AddVolunteersCommands()
            .AddVolunteersQueries()
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    private static IServiceCollection AddVolunteersCommands(this IServiceCollection services)
    {
        return services.Scan(scan => scan
            .FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses(classes => classes.AssignableToAny(
                typeof(ICommandHandler<,>),
                typeof(ICommandHandler<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());
    }
    
    private static IServiceCollection AddVolunteersQueries(this IServiceCollection services)
    {
        return services.Scan(scan => scan
            .FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());
    }
    
    public static IServiceCollection AddVolunteersInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<VolunteerWriteDbContext>();
        services.AddScoped<IVolunteersReadDbContext, VolunteerReadDbContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IVolunteersRepository, VolunteersRepository>();
        services.AddScoped<GetVolunteerByIdHandler>();
        services.AddScoped<UpdateMainInfoHandler>();
        services.AddScoped<DeleteVolunteerHandler>();
        services.AddScoped<CreateVolunteerHandler>();
        

        return services;
    }
}