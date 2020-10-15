﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Phrases.Application.Phrases.Commands.CreatePhrase;
using Phrases.Infrastructure.Configuration.Processing.Outbox;

namespace Phrases.IntegrationTests.SeedWork
{
    public class OutboxMessagesHelper
    {
        public static async Task<List<OutboxMessageDto>> GetOutboxMessages(IDbConnection connection)
        {
            const string sql = "SELECT " +
                               "[OutboxMessage].[Id], " +
                               "[OutboxMessage].[Type], " +
                               "[OutboxMessage].[Data] " +
                               "FROM [Phrase].[OutboxMessages] AS [OutboxMessage] " +
                               "ORDER BY [OutboxMessage].[OccurredOn]";

            var messages = await connection.QueryAsync<OutboxMessageDto>(sql);
            return messages.AsList();
        }

        public static T Deserialize<T>(OutboxMessageDto message) where T : class, INotification
        {
            Type type = Assembly.GetAssembly(typeof(PhraseCreatedNotification)).GetType(message.Type);
            return JsonConvert.DeserializeObject(message.Data, type) as T;
        }

      
    }
}
