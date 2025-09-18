namespace EnvironmentMonitoringSystem.Infrastructure.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>?> List();
        Task<T?> GetById(Guid id);
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
    }
}
