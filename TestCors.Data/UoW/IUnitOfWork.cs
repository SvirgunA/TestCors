using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace TestCors.Data.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task BeginTransactionAsync();
        void CommitTransaction();
        void RollbackTransaction();
        Task SaveAsync();
        Task ExecuteQueryAsync(RawSqlString query, params object[] parameters);
    }
}
