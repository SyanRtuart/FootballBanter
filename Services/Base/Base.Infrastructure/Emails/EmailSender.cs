using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Application.Emails;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Base.Infrastructure.Emails
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailsConfiguration _configuration;
        private readonly AuthMessageSenderOptions _options;

        public EmailSender(EmailsConfiguration configuration, IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            _configuration = configuration;
            _options = optionsAccessor.Value;
        }
        public Task SendEmail(EmailMessage message)
        {
            //_logger.Information(
            //    "Email sent. From: {From}, To: {To}, Subject: {Subject}, Content: {Content}.",
            //    _configuration.FromEmail,
            //    message.To,
            //    message.Subject,
            //    message.Content);
            return Execute(_options.SendGridKey, message.To, message.Subject, message.Content);
        }

        public Task Execute(string apiKey, string to, string subject, string content)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("ryan_7229@hotmail.co.uk", _options.SendGridUser),
                Subject = subject,
                PlainTextContent = content,
                HtmlContent = content
            };
            msg.AddTo(new EmailAddress("ryan_7229@hotmail.co.uk"));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }

    }
}
