using PetProject.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<ApplicationDbContext>();
var app = builder.Build();
app.MapControllers();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
