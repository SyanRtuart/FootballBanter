using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Base.Infrastructure;
using Dapper;
using Matches.Application.Matches.SharedModels;
using MediatR;

namespace Matches.Application.Matches.Queries.GetMatchById
{
    public class GetMatchByIdQueryHandler : IRequestHandler<GetMatchByIdQuery, MatchDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMatchByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }


        public async Task<MatchDto> Handle(GetMatchByIdQuery request, CancellationToken cancellationToken)
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
                        WHERE  ( m.Id = @Id )";

            var response = await connection.QueryAsync<MatchDto>(sql, new
            {
                request.Id
            });

            return response.SingleOrDefault();
        }
    }
}