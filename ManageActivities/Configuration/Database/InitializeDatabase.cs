using ManageActivities.Context;
using Microsoft.EntityFrameworkCore;

namespace ManageActivities.Configuration.Database;

public static class InitializeDatabase
{
    public static async Task InitializeDbAsync(this IServiceProvider serviceProvider)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        DatabaseContext databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        await databaseContext.Database.MigrateAsync();
    }
}