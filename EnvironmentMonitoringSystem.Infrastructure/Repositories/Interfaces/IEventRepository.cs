using EnvironmentMonitoringSystem.Domain.Models;

namespace EnvironmentMonitoringSystem.Infrastructure.Repositories.Interfaces
{
    public interface IEventRepository : IBaseRepository<Event>
    {
        Task<List<Event>> ListLastEvents();
    }
}
