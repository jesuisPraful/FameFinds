using System.Net.Mail;
using System.Net;

namespace FameFindsWebServices.Services
{
    public class EmailService
    {
        public string GenerateOtp()
        {
            return new Random().Next(100000, 999999).ToString();
        }
        public void SendOtpEmail(string toEmail, string otp)
        {
            var fromAddress = new MailAddress("your_email@example.com", "Your App");
            var toAddress = new MailAddress(toEmail);
            const string fromPassword = "your_email_password";
            string subject = "Your OTP Code";
            string body = $"Your OTP is: {otp}";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
