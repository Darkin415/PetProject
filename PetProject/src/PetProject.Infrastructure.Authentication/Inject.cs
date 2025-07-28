using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PetProject.Application.Authorization.DataModels;
using PetProject.Application.Database;
using PetProject.Infrastructure.DbContexts;

namespace PetProject.Infrastructure.Authentication;

public static class Inject
{
    public static IServiceCollection AddAuthorizationInfrastructure(
        this IServiceCollection services)
    {
        services
            .AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AuthorizationDbContext>();
        
        services.AddScoped<AuthorizationDbContext>();
        
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "test",
                    ValidAudience = "test",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("dasdfasdfasfaasdfasdfasdfasdfassdf")),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });
        
        services.AddAuthorization();
        
        return services;
    }
    
}