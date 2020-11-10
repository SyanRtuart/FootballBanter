using System.Text;

namespace Base.Infrastructure.Emails
{
    public class EmailsConfiguration
    {

        public EmailsConfiguration(string fromEmail, string sendGridUser, string sendGridKey)
        {
            FromEmail = fromEmail;
            SendGridUser = sendGridUser;
            SendGridKey = sendGridKey;
        }

        public string FromEmail { get; set; }
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
    }
}