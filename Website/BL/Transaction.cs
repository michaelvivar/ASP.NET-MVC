using BL.Services;
using DL;
using System;

namespace BL
{
    public static class Transaction
    {
        public static void Service<TService>(Action<TService> action) where TService : IService, new()
        {
            using (TService service = new TService())
            {
                action.Invoke(service);
            }
        }

        public static TResult Service<TService, TResult>(Func<TService, TResult> action) where TService : IService, new()
        {
            using (TService service = new TService())
            {
                return action.Invoke(service);
            }
        }
    }
}
