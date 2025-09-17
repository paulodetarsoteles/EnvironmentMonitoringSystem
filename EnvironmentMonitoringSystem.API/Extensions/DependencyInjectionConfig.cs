using EnvironmentMonitoringSystem.Infrastructure.Repositories;
using EnvironmentMonitoringSystem.Infrastructure.Repositories.Interfaces;
using EnvironmentMonitoringSystem.Infrastructure.Transactions;
using EnvironmentMonitoringSystem.Infrastructure.Transactions.Interfaces;

namespace EnvironmentMonitoringSystem.API.Extensions
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfig(this IServiceCollection services)
        {
            services.AddScoped<IUnityOfWork, UnityOfWork>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

        }
    }
}
