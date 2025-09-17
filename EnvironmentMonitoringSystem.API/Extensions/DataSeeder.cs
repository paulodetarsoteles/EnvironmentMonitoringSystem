using Microsoft.EntityFrameworkCore;

namespace EnvironmentMonitoringSystem.API.Extensions
{
    public static class DataSeeder
    {
        public static IApplicationBuilder UseDataSeeder<TDbContext>(this IApplicationBuilder app) where TDbContext : DbContext
        {
            try
            {
                using var scope = app.ApplicationServices.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();

                dbContext.Database.Migrate();

                return app;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro na aplicação das migrations: {ex.Message}");
            }
        }
    }
}
