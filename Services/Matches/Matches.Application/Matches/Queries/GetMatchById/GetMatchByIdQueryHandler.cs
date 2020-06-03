using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Base.Infrastructure;
using Dapper;
using Matches.Application.Matches.Queries.GetRecentMatchesByTeam;
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

            const string sql = @"SELECT m.id, 
                               team1.NAME AS HomeTeam, 
                               team.NAME  AS AwayTeam, 
                                m.statusid, 
                                m.utcdate, 
                                m.scorewinner, 
                                m.scorehometeam, 
                                m.scoreawayteam,
                                m.AwayTeamId,
                                m.HomeTeamId
                        FROM   match.matches AS m 
                               INNER JOIN match.teams AS team 
                                       ON m.awayteamid = team.id 
                               INNER JOIN match.teams AS team1 
                                       ON m.hometeamid = team1.id " +
                               "WHERE m.id = @id";
            
            var response = await connection.QueryAsync<MatchDto>(sql, new
            {
                request.Id
            });

            return response.SingleOrDefault();
        }
    }
}
