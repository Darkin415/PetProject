using PetProject.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<ApplicationDbContext>();
var app = builder.Build();
app.MapControllers();
app.Run();
