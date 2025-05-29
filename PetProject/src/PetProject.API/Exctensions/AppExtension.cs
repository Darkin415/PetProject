using PetProject.Infrastructure;
using Microsoft.EntityFrameworkCore;

public static class AppExtension
{
    public static async Task AplyMigration(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.EnsureCreatedAsync();
        await dbContext.Database.MigrateAsync();       
    }
}
