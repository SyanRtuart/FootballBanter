﻿using System;
using System.Threading.Tasks;
using Base.Infrastructure;
using Base.Infrastructure.DomainEventsDispatching;
using Dapper;
using Matches.Application.Configuration.Commands;
using Matches.Application.Contracts;
using Newtonsoft.Json;

namespace Matches.Infrastructure.Processing.InternalCommands
{
    public class CommandsScheduler : ICommandsScheduler
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public CommandsScheduler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task EnqueueAsync(ICommand command)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sqlInsert = "INSERT INTO [Match].[InternalCommands] ([Id], [EnqueueDate] , [Type], [Data]) VALUES " +
                                     "(@Id, @EnqueueDate, @Type, @Data)";

            await connection.ExecuteAsync(sqlInsert, new
            {
                command.Id,
                EnqueueDate = DateTime.UtcNow,
                Type = command.GetType().FullName,
                Data = JsonConvert.SerializeObject(command, new JsonSerializerSettings
                {
                    ContractResolver = new DomainEventsDispatcher.AllPropertiesContractResolver()
                })
            });
        }

        public async Task EnqueueAsync<T>(ICommand<T> command)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sqlInsert = "INSERT INTO [Match].[InternalCommands] ([Id], [EnqueueDate] , [Type], [Data]) VALUES " +
                                     "(@Id, @EnqueueDate, @Type, @Data)";

            await connection.ExecuteAsync(sqlInsert, new
            {
                command.Id,
                EnqueueDate = DateTime.UtcNow,
                Type = command.GetType().FullName,
                Data = JsonConvert.SerializeObject(command, new JsonSerializerSettings
                {
                    ContractResolver = new DomainEventsDispatcher.AllPropertiesContractResolver()
                })
            });
        }
    }
}