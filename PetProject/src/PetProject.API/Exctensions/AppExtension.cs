using Microsoft.EntityFrameworkCore;
using PetProject.Infrastructure.DbContexts;

namespace PetProject.API.Exctensions;

public static class AppExtension
{
    public static async Task AplyMigration(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();
        await dbContext.Database.EnsureCreatedAsync();
        await dbContext.Database.MigrateAsync();       
    }
}