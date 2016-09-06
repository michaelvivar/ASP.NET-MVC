using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class NotificationService : BaseService
    {
        public void SendEmail(string sendTo, string message)
        {
            Service<EmailService>(service => service.Send());
        }

        public void SendEmail(string[] sendTo, string message)
        {
            Service<EmailService>(service =>
            {
                sendTo.ToList().ForEach(o => service.Send());
            });
        }

        public void SendSMS(string sendTo, string message)
        {

        }

        public void Notify(int id, string message)
        {

        }
    }
}
