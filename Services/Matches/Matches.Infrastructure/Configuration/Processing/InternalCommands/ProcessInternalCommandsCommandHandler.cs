using System;
using System.Threading;
using System.Threading.Tasks;
using Base.Infrastructure;
using Dapper;
using Matches.Application.Configuration.Commands;
using Matches.Application.Contracts;
using MediatR;
using Newtonsoft.Json;

namespace Matches.Infrastructure.Configuration.Processing.InternalCommands
{
    internal class ProcessInternalCommandsCommandHandler : ICommandHandler<ProcessInternalCommandsCommand>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IMatchModule _matchModule;
        public ProcessInternalCommandsCommandHandler(
            ISqlConnectionFactory sqlConnectionFactory, IMatchModule matchModule)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _matchModule = matchModule;
        }

        public async Task<Unit> Handle(ProcessInternalCommandsCommand command, CancellationToken cancellationToken)
        {
            var connection = this._sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT " +
                               "[Command].[Type], " +
                               "[Command].[Data] " +
                               "FROM [Match].[InternalCommands] AS [Command] " +
                               "WHERE [Command].[ProcessedDate] IS NULL";
            var commands = await connection.QueryAsync<InternalCommandDto>(sql);

            var internalCommandsList = commands.AsList();

            foreach (var internalCommand in internalCommandsList)
            {
                Type type = Assemblies.Application.GetType(internalCommand.Type);
                dynamic commandToProcess = JsonConvert.DeserializeObject(internalCommand.Data, type);

                await _matchModule.ExecuteCommandAsync(commandToProcess);
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
