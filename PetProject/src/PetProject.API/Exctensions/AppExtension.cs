

using Microsoft.EntityFrameworkCore;
using PetProject.Volunteers.Infrastructure.DbContexts;

namespace PetProject.API.Exctensions;

public static class AppExtension
{
    public static async Task AplyMigration(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<VolunteerWriteDbContext>();
        await dbContext.Database.EnsureCreatedAsync();
        await dbContext.Database.MigrateAsync();       
    }
}