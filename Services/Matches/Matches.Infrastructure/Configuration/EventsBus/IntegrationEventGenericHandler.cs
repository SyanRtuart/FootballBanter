using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Base.Infrastructure;
using Base.Infrastructure.EventBus;
using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Matches.Infrastructure.Configuration.EventsBus
{
    public class IntegrationEventGenericHandler<T> : IIntegrationEventHandler<T> where T : IntegrationEvent
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public async Task Handle(T @event)
        {
            using (var scope = MatchesCompositionRoot.BeginLifetimeScope())
            {
                using (var connection = scope.Resolve<ISqlConnectionFactory>().GetOpenConnection())
                {
                    string type = @event.GetType().FullName;
                    var data = JsonConvert.SerializeObject(@event, new JsonSerializerSettings
                    {
                        ContractResolver = new AllPropertiesContractResolver()
                    });

                    var sql = "INSERT INTO [Matches].[InboxMessages] (Id, OccurredOn, Type, Data) " +
                              "VALUES (@Id, @OccurredOn, @Type, @Data)";

                    await connection.ExecuteScalarAsync(sql, new
                    {
                        @event.Id,
                        @event.OccurredOn,
                        type,
                        data
                    });
                }
            }
        }

        private class AllPropertiesContractResolver : DefaultContractResolver
        {
            protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
            {
                var props = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .Select(p => base.CreateProperty(p, memberSerialization))
                    .Union(type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                        .Select(f => base.CreateProperty(f, memberSerialization)))
                    .ToList();
                props.ForEach(p =>
                {
                    p.Writable = true;
                    p.Readable = true;
                });
                return props;
            }
        }


    }
}
