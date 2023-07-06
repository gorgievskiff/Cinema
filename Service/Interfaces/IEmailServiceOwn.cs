using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IEmailServiceOwn
    {
        public void SendEmail(Message message);
        MimeMessage CreateEmailMessage(Message message);
        void Send(MimeMessage mailMessage);
    }
}
