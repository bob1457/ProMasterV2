using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Net.Mail;
//using System.Web.Mail;
using System.Threading.Tasks;
using SendGrid;
using MailMessage = System.Net.Mail.MailMessage;




namespace ProMaster.Infrastructure.Email
{
    public class SMTPService
    {
        public void SendMail(string From, string To, string Subject, string Body, bool isHTML)
        {
            //MailMessage message = new MailMessage();

            //SmtpClient client = new SmtpClient("localhost");

            //message.From = new MailAddress(From);
            //message.To.Add (new MailAddress(To));
            //message.Subject = Subject;
            //message.Body = Body;
            //message.IsBodyHtml = false;

            //client.Send(message);   


            //********************************************
            //  Send Email Using SendGrid API C# Library 
            //********************************************

            var myMessage = new SendGridMessage();

            myMessage.AddTo(To);
            myMessage.From = new System.Net.Mail.MailAddress(From, "System Admin");
            myMessage.Subject = Subject;
            myMessage.Text = Body;

            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["emailService:Account"],
                                                   ConfigurationManager.AppSettings["emailService:Password"]);

            // Create a Web transport for sending email.
            var transportWeb = new Web(credentials);
 

            // Send the email.
            if (transportWeb != null)
            {
               transportWeb.DeliverAsync(myMessage);
            }
            else
            {
                //Trace.TraceError("Failed to create Web transport.");
                Task.FromResult(0);
            }


        }
    }
    
}
