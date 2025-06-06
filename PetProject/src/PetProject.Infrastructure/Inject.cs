using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetProject.Application.Volunteers;
using PetProject.Infrastructure.Interceptors;
using PetProject.Infrastructure.Repositories;
namespace PetProject.Infrastructure;
public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ApplicationDbContext>();

        services.AddScoped<IVolunteersRepository, VolunteersRepository>();

       

        return services;
    }
}
