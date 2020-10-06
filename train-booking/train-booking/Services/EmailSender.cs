using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using train_booking.Services.Interfaces;

namespace train_booking.Services
{
    public class EmailSender : IEmailSender
    {
        private IWebHostEnvironment _environment;
        private IHttpContextAccessor _httpContextAccessor;

        private string _title;
        private string _email;
        private string _password;
        private string _host;
        private int _port;

        public EmailSender(IConfiguration configuration, IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
        {
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;

            var mail = configuration.GetSection("Mail");

            _title = mail.GetValue<string>("Title");
            _email = mail.GetValue<string>("Address");
            _password = mail.GetValue<string>("Password");
            _host = mail.GetValue<string>("Host");
            _port = mail.GetValue<int>("Port");
        }

        public async Task SendDefaultEmailAsync(string email, string subject, string message, string buttonLink, string buttonText)
        {
            await SendEmailAsync(email, subject, CreateDeafaulHtmlBody(message, buttonLink, buttonText));
        }

        private async Task SendEmailAsync(string email, string subject, string htmlBody)
        {
            MimeMessage emailMessage = CreateMimeMessage(email, subject, htmlBody);

            using (var client = new SmtpClient())
            {
                try
                {
                    // during development
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    await client.ConnectAsync(_host, _port, SecureSocketOptions.StartTlsWhenAvailable);
                    await client.AuthenticateAsync(_email, _password);
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);
                }
                catch (Exception)
                { }
            }
        }

        private MimeMessage CreateMimeMessage(string email, string subject, string htmlBody)
        {
            MimeMessage emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_title, _email));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlBody };

            return emailMessage;
        }

        private string CreateDeafaulHtmlBody(string message, string buttonLink, string buttonText)
        {
            string body = "";
            var webRoot = _environment.WebRootPath;
            var htmlFile = Path.Combine(webRoot, "lib", "template", "email-letter-template.html");

            using (StreamReader reader = new StreamReader(htmlFile))
            {
                body = reader.ReadToEnd();
            }

            string domainUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.ToString()}";

            body = body.Replace("{CONTENT}", message);
            body = body.Replace("{domain-url}", domainUrl);
            body = body.Replace("{button-link}", buttonLink);
            body = body.Replace("{button-text}", buttonText);

            return body;
        }

    }
}
