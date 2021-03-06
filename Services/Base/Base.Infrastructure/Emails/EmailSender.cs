﻿using System.Threading.Tasks;
using Base.Application.Emails;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using Serilog;

namespace Base.Infrastructure.Emails
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailsConfiguration _emailsConfiguration;
        private readonly ILogger _logger;

        public EmailSender(ILogger logger,
            EmailsConfiguration configuration)
        {
            _logger = logger;
            _emailsConfiguration = configuration;
        }

        public Task SendEmail(EmailMessage message)
        {
            _logger.Information(
                $"Email sent. From: {_emailsConfiguration.FromEmail}, To: {message.To}, Subject: {message.Subject}, Content: {message.Content}.",
                _emailsConfiguration.FromEmail,
                message.To,
                message.Subject,
                message.Content);
            return Execute(_emailsConfiguration.SendGridKey, message.To, message.Subject, message.Content);
        }

        public Task Execute(string apiKey, string to, string subject, string content)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage
            {
                From = new EmailAddress(_emailsConfiguration.FromEmail, _emailsConfiguration.SendGridUser),
                Subject = subject,
                PlainTextContent = content,
                HtmlContent = content
            };

            msg.AddTo(new EmailAddress(to));

            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}