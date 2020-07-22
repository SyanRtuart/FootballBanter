using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Base.Infrastructure;
using Dapper;
using Matches.Application.Configuration.Queries;
using Matches.Application.Matches.SharedModels;

namespace Matches.Application.Matches.Queries.GetAllMatches
{
    public class GetAllMatchesQueryHandler : IQueryHandler<GetAllMatchesQuery, List<MatchDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetAllMatchesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<MatchDto>> Handle(GetAllMatchesQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = @"SELECT Id,
                               AwayTeamId,
                               HomeTeamId,
                               UtcDate,
                               ScoreWinner,
                               ScoreHomeTeam,
                               ScoreAwayTeam,
                               ExternalId,
                               Name,
                               Season,
                               Status
                        FROM   Match.Matches";

            var response = await connection.QueryAsync<MatchDto>(sql);

            return response.AsList();
        }
    }
}
