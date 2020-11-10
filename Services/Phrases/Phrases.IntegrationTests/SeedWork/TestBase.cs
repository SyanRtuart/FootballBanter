using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Base.Application.Emails;
using Base.Infrastructure.Emails;
using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using NSubstitute;
using NUnit.Framework;
using Phrases.Application.Contracts;
using Phrases.Domain.Phrase;
using Phrases.Infrastructure;
using Phrases.Infrastructure.Configuration;
using Phrases.Infrastructure.Configuration.Processing;
using Serilog;

namespace Phrases.IntegrationTests.SeedWork
{
    public class TestBase
    {
        protected string ConnectionString;

        protected ILogger Logger;

        protected IPhrasesModule PhraseModule;

        protected IEmailSender EmailSender;

        [SetUp]
        public async Task BeforeEachTest()
        {
            const string connectionStringEnvironmentVariable =
                "ASPNETCORE_FootballBanter_IntegrationTests_ConnectionString";
            ConnectionString = Environment.GetEnvironmentVariable(connectionStringEnvironmentVariable, EnvironmentVariableTarget.Machine);
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

            PhrasesStartup.Initialize(
                ConnectionString,
                new ExecutionContextMock(Guid.NewGuid()),
                Logger,
                new EmailsConfiguration("from@email.com"),
                "key",
                null,
                null);

            PhraseModule = new PhrasesModule();
        }

        private static async Task ClearDatabase(IDbConnection connection)
        {
            const string sql = "DELETE FROM [Phrase].[InboxMessages] " +
                               "DELETE FROM [Phrase].[InternalCommands] " +
                               "DELETE FROM [Phrase].[OutboxMessages] " +
                               "DELETE FROM [Phrase].[Phrases] " +
                               "DELETE FROM [Phrase].[PhraseVoteHistory] ";

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