using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Base.Infrastructure;
using Base.Infrastructure.EventBus;
using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace UserAccess.Infrastructure.Configuration.EventsBus
{
    public class IntegrationEventGenericHandler<T> : IIntegrationEventHandler<T> where T : IntegrationEvent
    {
        public async Task<bool> Handle(T @event)
        {
            try
            {
                using (var scope = UserAccessCompositionRoot.BeginLifetimeScope())
                {
                    using (var connection = scope.Resolve<ISqlConnectionFactory>().GetOpenConnection())
                    {
                        var type = @event.GetType().FullName;
                        var data = JsonConvert.SerializeObject(@event, new JsonSerializerSettings
                        {
                            ContractResolver = new AllPropertiesContractResolver()
                        });

                        var sql = "INSERT INTO [UserAccess].[InboxMessages] (Id, OccurredOn, Type, Data) " +
                                  "VALUES (@Id, @OccurredOn, @Type, @Data)";

                        await connection.ExecuteScalarAsync(sql, new
                        {
                            @event.Id,
                            @event.OccurredOn,
                            type,
                            data
                        });

                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException($"Unable to process message {@event}. Exception : {e.Message}");
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
