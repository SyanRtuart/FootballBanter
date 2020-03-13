using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Base.Infrastructure;
using Dapper;
using MediatR;

namespace Matches.Application.Teams.Queries.GetAllTeams
{
    public class GetAllTeamsQueryHandler : IRequestHandler<GetAllTeamsQuery, List<TeamDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetAllTeamsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<TeamDto>> Handle(GetAllTeamsQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT * " +
                               "FROM Match.Teams ";
            
            var teams = await connection.QueryAsync<TeamDto>(sql);

            return teams.AsList();
        }
    }
}