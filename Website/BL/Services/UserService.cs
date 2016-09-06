using DL;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class UserService : BaseService, IService
    {
        public string GetEmailAddress(int id)
        {
            return Db.UniOfWork(uow => "michaelvivar@mail.com");
        }
    }
}
