using Sligo.Areas.Area.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace Sligo.Business
{
    public class MailBusiness
    {
        
  

        public static async Task<bool> SendMail(MailViewModel viewmodel,List<string>To)
        {

            try
            {
                SmtpSection section = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                var message = new MailMessage();
                message.From = new MailAddress(section.From, viewmodel.MailEmail);
                message.To.Add(new MailAddress(section.Network.UserName));
                message.Subject = viewmodel.MailSubject;
                message.Body = viewmodel.MailBody;

                using (var client = new SmtpClient())
                {
                    client.EnableSsl = section.Network.EnableSsl;
                    client.UseDefaultCredentials = section.Network.DefaultCredentials;
                    client.Credentials = new NetworkCredential(section.Network.UserName, section.Network.Password);
                    client.Host = section.Network.Host;
                    client.Port = section.Network.Port;
                    
                    client.SendCompleted += Client_SendCompleted;

                    await client.SendMailAsync(message);

                    client.Dispose();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        private static void Client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}