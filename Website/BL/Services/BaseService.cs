using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class BaseService : IService
    {
        protected readonly IUnitOfWork _unitOfWork = new UnitOfWork(new MyContext());

        public void Dispose()
        {
            ((UnitOfWork)_unitOfWork).Dispose();
        }

        protected IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            return _unitOfWork.Repository<TEntity>();
        }

        protected void Repository<TEntity>(Action<IRepository<TEntity>> action) where TEntity : class
        {
            action.Invoke(Repository<TEntity>());
        }

        protected void Repository<TEntity>(Action<IRepository<TEntity>, IUnitOfWork> action) where TEntity : class
        {
            action.Invoke(Repository<TEntity>(), _unitOfWork);
        }
    }

    public interface IService : IDisposable
    {

    }
}
