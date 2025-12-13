using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.Repository_Interfaces;

namespace E_commerce_Infrastructure.Services
{
    internal class EmailService : IEmailService
    {
        public async Task SendMessageAsync(string toEmail, string Message)
        {
            // Sender information
            var fromAddress = new MailAddress("someoneegy2018@gmail.com", "Amr Bank System");
            string fromPassword = "ptujpywvixcutxur"; // App Password from Gmail

            // Reciever information
            var toAddress = new MailAddress(toEmail);

            // Message Content
            string subject = "Message from Amr Bank";
            string body = Message;

            // SMTP Settings
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            // Send Message
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                await smtp.SendMailAsync(message);
            }
        }

    }
}
