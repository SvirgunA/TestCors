using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestCors.Data.EF;

namespace TestCors.Data.UoW.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private static readonly object _createRepositoryLock = new object();

        private readonly DbContext _context;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        private IDbContextTransaction _transaction;

        private bool _disposed;
        private bool _transactionClosed;

        public UnitOfWork(TestCorsContext context)
        {
            _context = context;
        }

        #region IUnitOfWork
        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (!_repositories.ContainsKey(typeof(TEntity)))
            {
                lock (_createRepositoryLock)
                {
                    if (!_repositories.ContainsKey(typeof(TEntity)))
                    {
                        CreateRepository<TEntity>();
                    }
                }
            }

            return _repositories[typeof(TEntity)] as IRepository<TEntity>;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
            _transactionClosed = false;
        }

        public void CommitTransaction()
        {
            _transaction?.Commit();
            _transactionClosed = true;
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            _transactionClosed = true;
        }

        private void CreateRepository<TEntity>() where TEntity : class
        {
            _repositories.Add(typeof(TEntity), new Repository<TEntity>(_context));
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task ExecuteQueryAsync(RawSqlString query, params object[] parameters)
        {
            return _context.Database.ExecuteSqlCommandAsync(query, parameters);
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            if (!_disposed) Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_transaction != null && !_transactionClosed)
                {
                    RollbackTransaction();
                }
                _context?.Dispose();
                _disposed = true;
            }
        }

        #endregion
    }
}
