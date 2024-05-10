namespace ManageActivities.Configuration.Application;

public static class CorsConfiguration
{
    private const string WebsiteCors = "_WebsiteCors";

    public static void AddCorsPolicies(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddCors(options =>
        {
            options.AddPolicy(WebsiteCors,
                builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
        });
    }

    public static void UseCorsPolicies(this WebApplication webApplication)
    {
        webApplication.UseCors(WebsiteCors);
    }
}