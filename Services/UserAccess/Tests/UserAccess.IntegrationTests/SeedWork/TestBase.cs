using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Base.Application.Emails;
using Base.Infrastructure.Emails;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Serilog;
using Serilog.Extensions.Logging;
using UserAccess.Application.Contracts;
using UserAccess.Infrastructure;
using UserAccess.Infrastructure.Configuration;
using UserAccess.Infrastructure.Configuration.Processing;
using UserAccess.Infrastructure.Persistence;

namespace UserAccess.IntegrationTests.SeedWork
{
    public class TestBase
    {
        protected string ConnectionString;

        protected ILogger Logger;

        protected IUserAccessModule UserAccessModule;

        protected IEmailSender EmailSender;

        protected ContainerBuilder Builder;

        protected ICommandExecutor CommandExecutor;

        protected IQueryExecutor QueryExecutor;

        [SetUp]
        public async Task BeforeEachTest()
        {
            const string connectionStringEnvironmentVariable =
                "ASPNETCORE_FootballBanter_IntegrationTests_ConnectionString";
            ConnectionString = Environment.GetEnvironmentVariable(connectionStringEnvironmentVariable, EnvironmentVariableTarget.User);
            if (ConnectionString == null)
            {
                throw new ApplicationException(
                    $"Define connection string to integration tests database using environment variable: {connectionStringEnvironmentVariable}");
            }


            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                await ClearDatabase(sqlConnection);
            }

            Logger = Substitute.For<ILogger>();
            EmailSender = Substitute.For<IEmailSender>();
            Builder = new ContainerBuilder();

            UserAccessStartup.Initialize(
                ConnectionString,
                new ExecutionContextMock(Guid.NewGuid()),
                Logger,
                new EmailsConfiguration("from@email.com"),
                "key",
                EmailSender,
                Builder);

            var container = Builder.Build();
            var mediator = container.Resolve<IMediator>();

            CommandExecutor = new CommandExecutor(mediator);
            QueryExecutor = new QueryExecutor(mediator);
            UserAccessModule = new UserAccessModule(CommandExecutor, QueryExecutor);
        }

        private static async Task ClearDatabase(IDbConnection connection)
        {
            const string sql = "DELETE FROM [users].[InboxMessages] " +
                               "DELETE FROM [users].[InternalCommands] " +
                               "DELETE FROM [users].[OutboxMessages] " +
                               "DELETE FROM [users].[UserRegistrations] " +
                               "DELETE FROM [users].[Users] " +
                               "DELETE FROM [users].[RolesToPermissions] " +
                               "DELETE FROM [users].[UserRoles] " +
                               "DELETE FROM [users].[Permissions] ";

            await connection.ExecuteScalarAsync(sql);
        }

        protected async Task<T> GetLastOutboxMessage<T>() where T : class, INotification
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var messages = await OutboxMessagesHelper.GetOutboxMessages(connection);

                return OutboxMessagesHelper.Deserialize<T>(messages.Last());
            }
        }
    }
}