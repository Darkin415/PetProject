using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetProject.Application.Volunteers.Create.SocialList;
using PetProject.Application.Volunteers.Create.Volunteer;
using PetProject.Application.Volunteers.CreateVolunteer;
using PetProject.Application.Volunteers.Delete;
using PetProject.Application.Volunteers.UpdateMainInfo;

namespace PetProject.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerHandler>();
        services.AddScoped<UpdateMainInfoHandler>();
        services.AddScoped<DeleteVolunteerHandler>();
        services.AddScoped<UpdateSocialListHandler>();

        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);
        return services;
    }
}
