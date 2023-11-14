using Data.Entity;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
namespace Bussines.Services
{
    public class EmailService : IEmailService
    {
        public bool Email(EmailEntity email)
        {
            try
            {
                var emailTo = new MimeMessage();
                emailTo.From.Add(MailboxAddress.Parse(Environment.GetEnvironmentVariable("EmailFrom")));
                emailTo.To.Add(MailboxAddress.Parse(email.Email));
                emailTo.Subject = email.Subject;
                emailTo.Body = new TextPart(TextFormat.Html)
                {
                    Text =email.Body 
                };

                var smtp = new SmtpClient();
                var emailPort = int.Parse(Environment.GetEnvironmentVariable("EmailPort"));
                smtp.Connect(Environment.GetEnvironmentVariable("EmailHost"), emailPort, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate(Environment.GetEnvironmentVariable("EmailFrom"), Environment.GetEnvironmentVariable("EmailPassword"));
                smtp.Send(emailTo);
                smtp.Disconnect(true);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
}
