namespace EnvironmentMonitoringSystem.Infrastructure.Transactions.Interfaces
{
    public interface IUnityOfWork : IDisposable
    {
        Task IniciarTransacao();
        Task<bool> Commit();
    }
}
