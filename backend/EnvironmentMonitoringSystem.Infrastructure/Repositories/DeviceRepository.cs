using EnvironmentMonitoringSystem.Domain.Models;
using EnvironmentMonitoringSystem.Infrastructure.Contexts;
using EnvironmentMonitoringSystem.Infrastructure.Repositories.Interfaces;
using EnvironmentMonitoringSystem.Infrastructure.Transactions.Interfaces;

namespace EnvironmentMonitoringSystem.Infrastructure.Repositories
{
    public class DeviceRepository(AppDbContext context, IUnityOfWork unityOfWork) : BaseRepository<Device>(context, unityOfWork), IDeviceRepository
    {
    }
}
