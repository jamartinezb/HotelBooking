using RestSharp;
using RestSharp.Authenticators;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;

namespace HotelBookingApi.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> SendReservationEmailAsyncGoo(string recipientEmail, string subject, string messageBody)
        {
            var mailSettings = _configuration.GetSection("MailService");

            var message = new MailMessage();
            message.From = new MailAddress(mailSettings["email"]);
            message.To.Add(recipientEmail);
            message.Subject = subject;
            message.Body = messageBody;
            message.IsBodyHtml = true;

            using var smtpClient = new SmtpClient(mailSettings["smtp"], Int32.Parse(mailSettings["puerto"]))
            {
                Credentials = new NetworkCredential(mailSettings["email"], mailSettings["clave"]),
                EnableSsl = true
            };

            await smtpClient.SendMailAsync(message);
            return true;
        }
    }
}

