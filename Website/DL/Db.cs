using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public static class Db
    {
        private static MyContext StaticContext;
        private static UnitOfWork StaticUnitOfWork;

        public static TOut UniOfWork<TOut>(Func<IUnitOfWork, TOut> action)
        {
            if (StaticContext != null)
            {
                return action.Invoke(StaticUnitOfWork);
            }
            else
            {
                using (MyContext context = new MyContext())
                {
                    StaticContext = context;
                    StaticUnitOfWork = new UnitOfWork(context);
                    var o = action.Invoke(StaticUnitOfWork);
                    StaticContext.SaveChanges();
                    StaticContext = null;
                    StaticUnitOfWork = null;
                    return o;
                }
            }
        }

        public static void UniOfWork(Action<IUnitOfWork> action)
        {
            if (StaticContext != null)
            {
                action.Invoke(StaticUnitOfWork);
            }
            else
            {
                using (MyContext context = new MyContext())
                {
                    StaticContext = context;
                    StaticUnitOfWork = new UnitOfWork(context);
                    action.Invoke(StaticUnitOfWork);
                    StaticContext.SaveChanges();
                    StaticContext = null;
                    StaticUnitOfWork = null;
                }
            }
        }
    }
}
