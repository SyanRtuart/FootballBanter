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

namespace UserAccess.Infrastructure.Configuration.Processing.InternalCommands
{
    internal class ProcessInternalCommandsCommandHandler : ICommandHandler<ProcessInternalCommandsCommand>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly ICommandExecutor _commandExecutor;

        public ProcessInternalCommandsCommandHandler(
            ISqlConnectionFactory sqlConnectionFactory, ICommandExecutor commandExecutor)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _commandExecutor = commandExecutor;
        }

        public async Task<Unit> Handle(ProcessInternalCommandsCommand command, CancellationToken cancellationToken)
        {
            var connection = this._sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT " +
                               "[Command].[Type], " +
                               "[Command].[Data] " +
                               "FROM [user].[InternalCommands] AS [Command] " +
                               "WHERE [Command].[ProcessedDate] IS NULL";
            var commands = await connection.QueryAsync<InternalCommandDto>(sql);

            var internalCommandsList = commands.AsList();

            foreach (var internalCommand in internalCommandsList)
            {
                Type type = Assemblies.Application.GetType(internalCommand.Type);
                dynamic commandToProcess = JsonConvert.DeserializeObject(internalCommand.Data, type);

                await _commandExecutor.Execute(commandToProcess);
            }

            return Unit.Value;
        }

        private class InternalCommandDto
        {
            public string Type { get; set; }

            public string Data { get; set; }
        }
    }
}
