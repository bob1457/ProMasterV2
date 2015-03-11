using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
//using System.Web.Mail;
using MailMessage = System.Net.Mail.MailMessage;


namespace ProMaster.Infrastructure.Email
{
    public class SMTPService
    {
        public void SendMail(string From, string To, string Subject, string Body, bool isHTML)
        {
            MailMessage message = new MailMessage();

            SmtpClient client = new SmtpClient("localhost");

            message.From = new MailAddress(From);
            message.To.Add (new MailAddress(To));
            message.Subject = Subject;
            message.Body = Body;
            message.IsBodyHtml = false;

            client.Send(message);            
            
        }
    }
    
}
