using EnvironmentMonitoringSystem.Infrastructure.Contexts;
using EnvironmentMonitoringSystem.Infrastructure.Transactions.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace EnvironmentMonitoringSystem.Infrastructure.Transactions
{
    public class UnityOfWork(AppDbContext db) : IUnityOfWork
    {
        private readonly AppDbContext _DbPedidoProdutoCliente = db;
        private IDbContextTransaction? DbContextTransaction { get; set; }

        public async Task IniciarTransacao()
        {
            DbContextTransaction = await _DbPedidoProdutoCliente.Database.BeginTransactionAsync();
        }

        public async Task<bool> Commit()
        {
            var ret = await _DbPedidoProdutoCliente.SaveChangesAsync() > 0;
            DbContextTransaction?.Commit();
            return ret;
        }

        public void Dispose()
        {
            _DbPedidoProdutoCliente?.Dispose();
            DbContextTransaction?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
