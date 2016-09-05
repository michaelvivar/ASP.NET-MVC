using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Helpers
{
    public static class PaginationHelper
    {
        public static int Skip(int per, int page)
        {
            if (page > 1)
            {
                return (page - 1) * per;
            }
            return 0;
        }
    }
}