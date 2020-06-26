using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Base.Application.Emails;
using Base.Infrastructure.Emails;

namespace UserAccess.Infrastructure.Configuration.Email
{
    internal class EmailModule : Module
    {
        private readonly IEmailSender _emailSender;
        private readonly EmailsConfiguration _configuration;

        public EmailModule(
            EmailsConfiguration configuration,
            IEmailSender emailSender)
        {
            _configuration = configuration;
            _emailSender = emailSender;
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (_emailSender != null)
            {
                builder.RegisterInstance(_emailSender);
            }
            else
            {
                builder.RegisterType<EmailSender>()
                    .As<IEmailSender>()
                    .WithParameter("configuration", _configuration)
                    .InstancePerLifetimeScope();
            }
        }
    }
}
