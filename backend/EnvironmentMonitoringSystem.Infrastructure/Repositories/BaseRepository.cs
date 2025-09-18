using EnvironmentMonitoringSystem.Domain.Interfaces;
using EnvironmentMonitoringSystem.Infrastructure.Contexts;
using EnvironmentMonitoringSystem.Infrastructure.Repositories.Interfaces;
using EnvironmentMonitoringSystem.Infrastructure.Transactions.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentMonitoringSystem.Infrastructure.Repositories
{
    public class BaseRepository<T>(AppDbContext context, IUnityOfWork unityOfWork) : IBaseRepository<T> where T : class, IEntity
    {
        protected readonly AppDbContext _context = context;
        private readonly IUnityOfWork _unityOfWork = unityOfWork;

        public virtual async Task<List<T>?> List()
        {
            return await _context.Set<T>()
                .Where(c => c.DeletionDate == null)
                .OrderBy(c => c.Id)
                .ToListAsync();
        }

        public virtual async Task<T?> GetById(Guid id)
        {
            return await _context.Set<T>()
                .Where(e => e.Id == id && e.DeletionDate == null)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> Add(T entity)
        {
            await _unityOfWork.IniciarTransacao();

            await _context.AddAsync(entity);

            var result = await _unityOfWork.Commit();

            return result;
        }

        public async Task<bool> Update(T entity)
        {
            await _unityOfWork.IniciarTransacao();

            _context.Update(entity);

            var result = await _unityOfWork.Commit();

            return result;
        }

        public async Task<bool> Delete(T entity)
        {
            await _unityOfWork.IniciarTransacao();

            _context.Update(entity);

            var result = await _unityOfWork.Commit();

            return result;
        }
    }
}