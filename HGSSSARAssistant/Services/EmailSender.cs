using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;


namespace HGSSSARAssistant.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string text)
        {
            MailMessage m = new MailMessage();
            SmtpClient sc = new SmtpClient();

            try
            {
                m.From = new MailAddress("hgssarassistant@gmail.com");
                m.To.Add(new MailAddress(email));

                m.Subject = subject;
                m.IsBodyHtml = true;
                m.Body = text;
                              
                sc.Host = "smtp.gmail.com";
                sc.Port = 587;
                sc.Credentials = new System.Net.NetworkCredential("hgssarassistant@gmail.com", "jud5achug?Da");
                sc.EnableSsl = true;
                return sc.SendMailAsync(m);
            }
            catch (Exception ex)
            {
                return Task.FromException(ex);
            }
        }
    }
}
