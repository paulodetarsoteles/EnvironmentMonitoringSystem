using EnvironmentMonitoringSystem.Application.Services;
using EnvironmentMonitoringSystem.Application.Services.Interfaces;
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
            services.AddHttpClient();
            services.AddScoped<IUnityOfWork, UnityOfWork>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IEventRepository, EventRepository>();

            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IEventService, EventService>();
        }
    }
}
