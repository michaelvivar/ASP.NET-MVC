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
    }
}
