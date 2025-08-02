using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using PetProejct.Volunteers.Application.Commands.CreateVolunteer;
using PetProejct.Volunteers.Application.Commands.DeleteVolunteer;
using PetProject.Accounts.Application;
using PetProject.Accounts.Application.Authorization;
using PetProject.Accounts.Application.Commands.Login;
using PetProject.Accounts.Application.Commands.Register;
using PetProject.API.Middlewares;
using Serilog;
using Serilog.Events;
using PetProject.Accounts.Infrastructure;
using PetProject.API;
using PetProject.API.Exctensions;
using PetProject.Contracts;
using PetProject.Core.Database;
using PetProject.Volunteers.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Debug()
    .WriteTo.Seq(builder.Configuration.GetConnectionString("Seq")
                 ?? throw new ArgumentNullException("Seq"))
    .Enrich.WithEnvironmentName()
    .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
    .CreateLogger();

builder.Services.AddHttpLogging();

builder.Services.AddScoped<LoginHandler>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo {
        Title = "My API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
builder.Services.AddSerilog();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
builder.Services
    .AddAuthorizationInfrastructure(builder.Configuration); 

builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

builder.Services.AddScoped<RegisterUserHandler>();

builder.Services.AddScoped<CreateVolunteerHandler>();

builder.Services.AddValidatorsFromAssembly(typeof(CreateVolunteerCommandValidator).Assembly);

builder.Services.AddScoped<DeleteVolunteerHandler>();

builder.Services.AddVolunteersInfrastructure();

builder.Services.AddSpeciesModule();

builder.Services.AddFilesModule(builder.Configuration);

builder.Services.AddSingleton<IAuthorizationHandler, CreateIssueRequirementHandler>();


var app = builder.Build();

app.UseExceptionMiddleware();
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    await app.AplyMigration();

}

app.UseHttpLogging();
app.MapControllers();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthorization();

app.Run();