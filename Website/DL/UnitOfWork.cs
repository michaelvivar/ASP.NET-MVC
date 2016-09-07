using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        List<RepositoryContainer> _repositories = new List<RepositoryContainer>();

        internal UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public void Repository<TEntity>(Action<IRepository<TEntity>> action) where TEntity : class
        {
            if (_repositories.Any(o => o.Type == typeof(TEntity)))
            {
                action.Invoke((IRepository<TEntity>)_repositories.Find(o => o.Type == typeof(TEntity)).Repository);
            }
            else
            {
                var repository = new Repository<TEntity>(_context);
                _repositories.Add(new RepositoryContainer { Type = typeof(TEntity), Repository = repository });
                action.Invoke(repository);
            }
        }

        public TOut Repository<TEntity, TOut>(Func<IRepository<TEntity>, TOut> action) where TEntity : class
        {
            if (_repositories.Any(o => o.Type == typeof(TEntity)))
            {
                return action.Invoke((IRepository<TEntity>)_repositories.Find(o => o.Type == typeof(TEntity)).Repository);
            }
            else
            {
                var repository = new Repository<TEntity>(_context);
                _repositories.Add(new RepositoryContainer { Type = typeof(TEntity), Repository = repository });
                return action.Invoke(repository);
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }

    public interface IUnitOfWork
    {
        void SaveChanges();
        void Repository<TEntity>(Action<IRepository<TEntity>> action) where TEntity : class;
        TOut Repository<TEntity, TOut>(Func<IRepository<TEntity>, TOut> action) where TEntity : class;
    }

    internal class RepositoryContainer
    {
        internal Type Type { get; set; }
        internal object Repository { get; set; }
    }
}
