using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Base.Infrastructure;
using Dapper;
using Matches.Application.Configuration.Queries;

namespace Matches.Application.Teams.Queries.GetTeam
{
    public class GetTeamQueryHandler : IQueryHandler<GetTeamQuery, TeamDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetTeamQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<TeamDto> Handle(GetTeamQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = @"SELECT Id,
                                       Name,
                                       StadiumDescription,
                                       StadiumLocation,
                                       StadiumName,
                                       Country,
                                       Description,
                                       Facebook,
                                       FormedYear,
                                       Instagram,
                                       League,
                                       Logo,
                                       Manager,
                                       ExternalId
                                FROM   Match.Teams
                                WHERE  ( Id = @Id)";

            var teams = await connection.QueryAsync<TeamDto>(sql, new
            {
                request.Id
            });

            return teams.FirstOrDefault();
        }
    }
}
