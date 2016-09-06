using BL.Services;
using DL;
using System;

namespace BL
{
    public static class Transaction
    {
        public static void Service<TService>(Action<TService> action) where TService : IService, new()
        {
            //TService service = (TService)Activator.CreateInstance(typeof(TService), uow);
            TService service = new TService();
            action.Invoke(service);
        }

        public static TOut Service<TService, TOut>(Func<TService, TOut> action) where TService : IService, new()
        {
            //TService service = (TService)Activator.CreateInstance(typeof(TService), uow);
            TService service = new TService();
            return action.Invoke(service);
        }
    }
}
