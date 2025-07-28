using PetProject.Infrastructure;
using PetProject.Application;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using PetProject.API.Middlewares;
using Serilog;
using Serilog.Events;
using PetProject.Infrastructure.Providers;
using PetProject.Application.Providers;
using PetProject.Application.Database;
using PetProject.Application.Volunteers.Create.Pet.Breed;
using PetProject.Application.Volunteers.Create.Pet.DeleteBreed;
using PetProject.Application.Volunteers.Create.Pet.DeleteSpecies;
using PetProject.Application.Volunteers.Create.Pet.GetBreedBySpeciesId;
using PetProject.Application.Volunteers.Create.Pet.GetPets;
using PetProject.Application.Volunteers.Create.Pet.GetSpecies;
using PetProject.Application.Volunteers.Create.Pet.MovePet;
using PetProject.Application.Volunteers.Create.Species;
using PetProject.Infrastructure.Repositories;

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
builder.Services.AddScoped<GetPetByIdHandler>();
builder.Services.AddScoped<MovePetHandler>();
builder.Services.AddScoped<CreateSpeciesHandler>();
builder.Services.AddScoped<CreateBreedHandler>();
builder.Services.AddScoped<DeleteBreedHandler>();
builder.Services.AddScoped<DeleteSpeciesHandler>();
builder.Services.AddScoped<GetSpeciesWithPaginationHandler>();
builder.Services.AddScoped<GetBreedBySpeciesIdHandler>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSerilog();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

var app = builder.Build();

app.UseExceptionMiddleware();
app.UseSerilogRequestLogging();

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

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication();
    .AddAuthorizationInfrastructure(builder.Configuration);




app.UseHttpLogging();
app.MapControllers();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.Run();