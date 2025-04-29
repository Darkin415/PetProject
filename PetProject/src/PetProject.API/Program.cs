using PetProject.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();
builder.Services.AddScoped<ApplicationDbContext>();
app.MapControllers();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
