
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace QLTBCNTT.Services
{
    public class SmtpSettings
    {
        public string Host { get; set; } = "";
        public int Port { get; set; }
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public bool UseSsl { get; set; }
    }

    public class EmailService
    {
        private readonly SmtpSettings _smtp;

        public EmailService(IOptions<SmtpSettings> smtp)
        {
            _smtp = smtp.Value;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using var client = new SmtpClient(_smtp.Host, _smtp.Port)
            {
                Credentials = new NetworkCredential(_smtp.Email, _smtp.Password),
                EnableSsl = _smtp.UseSsl
            };

            var mail = new MailMessage(_smtp.Email, to, subject, body)
            {
                IsBodyHtml = true
            };

            await client.SendMailAsync(mail);
        }
    }
}
