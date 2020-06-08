using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Application.Emails
{
    public struct EmailMessage
    {
        public string To { get; }

        public string Subject { get; }

        public string Content { get; }

        public EmailMessage(
            string to,
            string subject,
            string content)
        {
            To = to;
            Subject = subject;
            Content = content;
        }
    }
}
