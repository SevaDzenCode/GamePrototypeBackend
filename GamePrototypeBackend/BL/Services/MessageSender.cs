using GamePrototypeBackend.BL.Models;
using GamePrototypeBackend.BL.Services.Interfaces;
using GamePrototypeBackend.Data.Models;
using MailKit.Net.Smtp;
using MimeKit;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace GamePrototypeBackend.BL.Services
{
    public class MessageSender : IMessageSender
    {
        public async Task SendEmailAsync(string email, string message)
        {   
            var emailMessage = new MimeMessage();
            var smtp = new SmtpModel();
            emailMessage.From.Add(new MailboxAddress("Email confirmation", smtp.gmailSender));
            emailMessage.To.Add(new MailboxAddress("", email));

            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtp.smtpGmail, smtp.smtpPort, true);
                await client.AuthenticateAsync(smtp.gmailSender, smtp.Password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
