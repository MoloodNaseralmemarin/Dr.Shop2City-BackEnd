using System.Net.Mail;
using DrShop2City.Infrastructure.Services.Interfaces;

namespace DrShop2City.Infrastructure.Services.Implementations
{
    public class SendEmail : IMailSender
    {
        public void Send(string to, string subject, string body)
        {
            //ایمیلی که میخوا از اون ارسال کنیم
            var defaultEmail = "mnaseri208@gmail.com";

            var mail = new MailMessage();

            var SmtpServer = new SmtpClient("stmp.gmail.com");

            mail.From = new MailAddress(defaultEmail, "فروشگاه پزشکی دوم شهر");

            mail.To.Add(to);

            mail.Subject = subject;

            mail.Body = body;

            mail.IsBodyHtml = true;

            SmtpServer.Port = 25;

            SmtpServer.Credentials = new System.Net.NetworkCredential(defaultEmail, "ASDasdFGHfgh123!@#");

            SmtpServer.EnableSsl = false;

            SmtpServer.Send(mail);
        }
    }
}

