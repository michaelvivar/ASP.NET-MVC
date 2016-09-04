using BL.Services;
using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
