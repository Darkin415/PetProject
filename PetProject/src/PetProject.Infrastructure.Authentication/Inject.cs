using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PetProject.Application.Authorizations.DataModels;
using PetProject.Application.Database;
using PetProject.Infrastructure.DbContexts;

namespace PetProject.Infrastructure.Authentication;

public static class Inject
{
    public static IServiceCollection AddAuthorizationInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ITokenProvider, JwtTokenProvider>();
        services.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.JWT));
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
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("asdfasdfasdfasdfasdfadfsadsfasdfasdfasdfdsf")),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "test",
                    ValidAudience = "test", 
                    ClockSkew = TimeSpan.Zero,
                };
            });
        services.AddAuthorization();
        return services;
    }


}