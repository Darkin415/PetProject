using Microsoft.AspNetCore.Builder;
using PetProject.Infrastructure;
using PetProject.Application;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using PetProject.Application.Volunteers.CreateVolunteer;
using PetProject.Application.Volunteers;
using PetProject.Infrastructure.Repositories;
using FluentValidation.AspNetCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using PetProject.API.Validation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddInfrastructure()
    .AddApplication();

builder.Services.AddFluentValidationAutoValidation(configuration =>
{
    configuration.OverrideDefaultResultFactoryWith<CustomResultFactory>();
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    await app.AplyMigration();

}

app.MapControllers();
app.UseHttpsRedirection();
app.UseAuthorization();

app.Run();