using Microsoft.AspNetCore.Builder;
using PetProject.Infrastructure;
using PetProject.Application;
using Microsoft.OpenApi.Models;  
using Swashbuckle.AspNetCore.SwaggerGen;
using PetProject.Application.Volunteers.CreateVolunteer;
using PetProject.Application.Volunteers;
using PetProject.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddInfrastructure()
    .AddApplication();



var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();
app.UseAuthorization();
app.Run();
