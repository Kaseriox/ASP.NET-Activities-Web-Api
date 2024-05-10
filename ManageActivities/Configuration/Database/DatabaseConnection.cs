using ManageActivities.Context;
using Microsoft.EntityFrameworkCore;

namespace ManageActivities.Configuration.Database;

public static class DatabaseConnection
{
    public static void ConnectToDatabase(this IServiceCollection serviceCollection,string connectionString)
    {
        serviceCollection.AddMySql<DatabaseContext>(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}