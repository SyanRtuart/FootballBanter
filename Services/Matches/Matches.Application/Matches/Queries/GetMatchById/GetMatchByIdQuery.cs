using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matches.Application.Matches.Queries.GetRecentMatchesByTeam;
using Matches.Application.Matches.SharedModels;
using MediatR;

namespace Matches.Application.Matches.Queries.GetMatchById
{
    public class GetMatchByIdQuery : IRequest<MatchDto>
    {
        public GetMatchByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get;}
    }
}
