using Autofac;
using Base.Infrastructure;
using Matches.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;

namespace Matches.Infrastructure.Configuration.DataAccess
{
    internal class DataAccessModule : Module
    {
        private readonly string _databaseConnectionString;
        private readonly ILoggerFactory _loggerFactory;

        internal DataAccessModule(string databaseConnectionString, ILoggerFactory loggerFactory)
        {
            _databaseConnectionString = databaseConnectionString;
            _loggerFactory = loggerFactory;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqlConnectionFactory>()
                .As<ISqlConnectionFactory>()
                .WithParameter("connectionString", _databaseConnectionString)
                .InstancePerLifetimeScope();

            builder
                .Register(c =>
                {
                    var dbContextOptionsBuilder = new DbContextOptionsBuilder<MatchContext>();
                    dbContextOptionsBuilder.UseSqlServer(_databaseConnectionString);

                    dbContextOptionsBuilder
                        .ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();

                    return new MatchContext(dbContextOptionsBuilder.Options, _loggerFactory);
                })
                .AsSelf()
                .As<DbContext>()
                .InstancePerLifetimeScope();

            var infrastructureAssembly = typeof(MatchContext).Assembly;

            builder.RegisterAssemblyTypes(infrastructureAssembly)
                .Where(type => type.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .FindConstructorsWith(new AllConstructorFinder());
            ;
        }
    }
}