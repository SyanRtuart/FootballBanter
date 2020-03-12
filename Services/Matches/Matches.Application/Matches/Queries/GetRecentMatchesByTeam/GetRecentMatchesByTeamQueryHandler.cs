using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Base.Infrastructure;
using Dapper;
using MediatR;

namespace Matches.Application.Matches.Queries.GetRecentMatchesByTeam
{
    public class GetRecentMatchesByTeamQueryHandler : IRequestHandler<GetRecentMatchesByTeamQuery, List<MatchDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetRecentMatchesByTeamQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<MatchDto>> Handle(GetRecentMatchesByTeamQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT *" +
                               "FROM match.matches AS m " +
                               "WHERE m.HomeTeamId = @TeamId";

            var response = await connection.QueryAsync<MatchDto>(sql, new
            {
                request.TeamId
            });

            return response.ToList();
        }
    }
}
