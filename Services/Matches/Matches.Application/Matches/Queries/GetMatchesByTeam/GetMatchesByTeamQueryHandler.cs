using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Base.Infrastructure;
using Dapper;
using Matches.Application.Configuration.Queries;
using Matches.Application.Matches.SharedModels;

namespace Matches.Application.Matches.Queries.GetMatchesByTeam
{
    public class GetMatchesByTeamQueryHandler : IQueryHandler<GetMatchesByTeamQuery, List<MatchDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMatchesByTeamQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<MatchDto>> Handle(GetMatchesByTeamQuery request,
            CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = @"SELECT m.Id,
                               homeTeam.Name AS HomeTeam,
                               awayTeam.Name AS AwayTeam,
                               m.Status,
                               m.UtcDate,
                               m.ScoreWinner,
                               m.ScoreHomeTeam,
                               m.ScoreAwayTeam,
                               m.AwayTeamId,
                               m.HomeTeamId,
                               m.Season,
                               m.Name,
                               homeTeam.Logo AS HomeTeamLogo,
                               awayTeam.Logo AS AwayTeamLogo
                        FROM   Match.Matches AS m
                               INNER JOIN Match.Teams AS homeTeam
                                 ON m.HomeTeamId = homeTeam.Id
                               INNER JOIN Match.Teams AS awayTeam
                                 ON m.AwayTeamId = awayTeam.Id
                        WHERE  ( m.HomeTeamId = @TeamId )
                                OR ( m.AwayTeamId = @TeamId)
                        ORDER BY m.UtcDate DESC";

            var response = await connection.QueryAsync<MatchDto>(sql, new
            {
                request.TeamId
            });

            return response.ToList();
        }
    }
}