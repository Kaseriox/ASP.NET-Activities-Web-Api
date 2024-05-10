using ManageActivities.Configuration.Authentication;
using ManageActivities.Configuration.Database;
using ManageActivities.Configuration.DI;

namespace ManageActivities.Configuration.Application;

public static class ApplicationConfiguration
{
    public static async Task Start(this WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Services.AddEndpointsApiExplorer();
        webApplicationBuilder.Services.AddControllers();
        webApplicationBuilder.Services.AddSwaggerGen();
        webApplicationBuilder.ConnectToDatabase();
        webApplicationBuilder.Services.AddRepositories();
        webApplicationBuilder.Services.AddJwt(webApplicationBuilder.Configuration);
        webApplicationBuilder.Services.AddCorsPolicies();
        webApplicationBuilder.Services.AddMemoryCache();
        WebApplication app = webApplicationBuilder.Build();
        await app.Services.InitializeDbAsync();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCorsPolicies();
        app.UseHsts();
        app.UseHttpsRedirection();
        app.MapControllers();
        await app.RunAsync();
    }

    private static void ConnectToDatabase(this WebApplicationBuilder webApplicationBuilder)
    {
        const string connectionStringSection = "MySqlConnection";
        string? connectionString = webApplicationBuilder.Configuration.GetConnectionString(connectionStringSection);
        if (connectionString is null)
        {
            throw new ApplicationException();
        }

        webApplicationBuilder.Services.ConnectToDatabase(connectionString);
    }
}