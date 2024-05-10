using ManageActivities.Repository.Implementation;
using ManageActivities.Repository.Interface;

namespace ManageActivities.Configuration.DI;

public static class InjectRepositories
{
    public static void AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IActivityRepository, ActivityRepository>()
            .AddScoped<ICategoryRepository, CategoryRepository>();
    }
}