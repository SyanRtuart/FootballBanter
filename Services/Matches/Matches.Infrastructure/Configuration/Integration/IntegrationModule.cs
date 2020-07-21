using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Matches.Infrastructure.Configuration.Integration
{
    public class IntegrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new HttpClient())
                .As<HttpClient>();

            builder.RegisterType<UrlsConfig>()
                .As<UrlsConfig>();

            builder.RegisterType<ImageConverter>()
                .As<ImageConverter>();

            builder.RegisterType<TheSportsDbApiClient>()
                .As<IIntegrationService>()
                .InstancePerLifetimeScope();
        }
    }
}
