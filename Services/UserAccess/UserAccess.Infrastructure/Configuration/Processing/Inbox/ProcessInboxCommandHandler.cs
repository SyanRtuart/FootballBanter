using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Base.Infrastructure;
using Dapper;
using MediatR;
using Newtonsoft.Json;
using UserAccess.Application.Configuration.Commands;

namespace UserAccess.Infrastructure.Configuration.Processing.Inbox
{
    internal class ProcessInboxCommandHandler : ICommandHandler<ProcessInboxCommand>
    {
        private readonly IMediator _mediator;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public ProcessInboxCommandHandler(IMediator mediator, ISqlConnectionFactory sqlConnectionFactory)
        {
            _mediator = mediator;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Unit> Handle(ProcessInboxCommand command, CancellationToken cancellationToken)
        {
            var connection = this._sqlConnectionFactory.GetOpenConnection();
            const string sql = "SELECT " +
                               "[InboxMessage].[Id], " +
                               "[InboxMessage].[Type], " +
                               "[InboxMessage].[Data] " +
                               "FROM [Users].[InboxMessages] AS [InboxMessage] " +
                               "WHERE [InboxMessage].[ProcessedDate] IS NULL";

            var messages = await connection.QueryAsync<InboxMessageDto>(sql);

            const string sqlUpdateProcessedDate = "UPDATE [Users].[InboxMessages] " +
                                                  "SET [ProcessedDate] = @Date " +
                                                  "WHERE [Id] = @Id";

            foreach (var message in messages)
            {
                var messageAssembly = AppDomain.CurrentDomain.GetAssemblies()
                    .SingleOrDefault(assembly => message.Type.Contains(assembly.GetName().Name));

                Type type = messageAssembly.GetType(message.Type);
                var request = JsonConvert.DeserializeObject(message.Data, type);

                try
                {
                    await _mediator.Publish((INotification)request, cancellationToken);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                await connection.ExecuteAsync(sqlUpdateProcessedDate, new
                {
                    Date = DateTime.UtcNow,
                    message.Id
                });
            }

            return Unit.Value;
        }
    }
}
