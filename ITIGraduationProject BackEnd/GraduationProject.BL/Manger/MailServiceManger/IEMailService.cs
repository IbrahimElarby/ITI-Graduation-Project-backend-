using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL.Manger.MailServiceManger
{
    public interface IEMailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }

}
