using System.Net.Mail;
using System.Net;
using FameFindsWebServices.Models;
using Microsoft.Extensions.Options;

namespace FameFindsWebServices.Services
{
    public interface IEmailService
    {
        void SendOtpEmail(string toEmail, string otp);
        string GenerateOtp();
    }
    public class EmailService: IEmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpOptions)
        {
            _smtpSettings = smtpOptions.Value;
        }

        public void SendOtpEmail(string toEmail, string otp)
        {
            var fromAddress = new MailAddress(_smtpSettings.UserName, "Fame Finds");

            var toAddress = new MailAddress(toEmail);
            var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = "OTP To Reset Your Password",
                Body = $"<html><body>" +
               $"<h2 style='color:#e74c3c;'>Do Not Share This OTP!</h2>" +
               $"<p>Your One-Time Password (OTP) is:</p>" +
               $"<h3 style='color:#68658c;'>{otp}</h3>" +
               $"<p>This OTP is valid for 5 minutes.</p>" +
               $"</body></html>",
                IsBodyHtml = true
            };

            using (var smtpClient = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port))
            {
                smtpClient.Credentials = new NetworkCredential(_smtpSettings.UserName, _smtpSettings.Password);
                smtpClient.EnableSsl = _smtpSettings.EnableSsl;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;

                smtpClient.Send(message);
            }
        }
        public string GenerateOtp()
        {
            return new Random().Next(100000, 999999).ToString();
        }
    }
}
