using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PetProject.Application.Authorization;
using PetProject.Application.Authorization.DataModels;
using PetProject.Application.Database;
using PetProject.Infrastructure.DbContexts;

namespace PetProject.Infrastructure.Authentication;

public static class Inject
{
    public static IServiceCollection AddAuthorizationInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ITokenProvider, JwtTokenProvider>();
        services
            .AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AuthorizationDbContext>();
        
        services.AddScoped<AuthorizationDbContext>();
        
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwtOptions = configuration.GetSection(JwtOptions.JWT).Get<JwtOptions>()
                    ?? throw new ApplicationException("Missing jwt configuration");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true 
                };
            });
        
        services.AddAuthorization(options =>
        {
            // options.DefaultPolicy = new AuthorizationPolicyBuilder()
            //     .RequireClaim("Role", "User")
            //     .RequireAuthenticatedUser()
            //     .Build();
            
            options.AddPolicy("CreateIssueRequirement", 
                policy => { policy.AddRequirements(new PermissionRequirement("create.issue")); });
            
            options.AddPolicy("CreateIssueRequirement", 
                policy => { policy.AddRequirements(new PermissionRequirement("create.issue")); });
            
            options.AddPolicy("CreateIssueRequirement", 
                policy => { policy.AddRequirements(new PermissionRequirement("create.issue")); });
            
            options.AddPolicy("CreateIssueRequirement", 
                policy => { policy.AddRequirements(new PermissionRequirement("create.issue")); });
        });

        services.AddSingleton<IAuthorizationHandler, CreateIssueRequirementHandler>();
        return services;
    }
    
}