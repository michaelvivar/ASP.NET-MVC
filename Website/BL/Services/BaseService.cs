﻿using System;

namespace BL.Services
{
    public abstract class BaseService : IService
    {
        public void Dispose()
        {
            
        }

        protected void Service<TService>(Action<TService> action) where TService : IService, new()
        {
            Transaction.Service(action);
        }

        protected TOut Service<TService, TOut>(Func<TService, TOut> action) where TService : IService, new()
        {
            return Transaction.Service(action);
        }
    }

    public interface IService : IDisposable
    {

    }
}
