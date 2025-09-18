using EnvironmentMonitoringSystem.Domain.Models;
using EnvironmentMonitoringSystem.Infrastructure.Contexts;
using EnvironmentMonitoringSystem.Infrastructure.Repositories.Interfaces;
using EnvironmentMonitoringSystem.Infrastructure.Transactions.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentMonitoringSystem.Infrastructure.Repositories
{
    public class EventRepository(AppDbContext context, IUnityOfWork unityOfWork) : BaseRepository<Event>(context, unityOfWork), IEventRepository
    {
        public async Task<List<Event>> ListLastEvents()
        {
            return await _context.Events
            .OrderByDescending(e => e.TimeStamp)
            .Take(20)
            .ToListAsync();
        }
    }
}
