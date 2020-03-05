﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Teams.Domain.Aggregates;
using Teams.Domain.Aggregates.TeamAggregate;

namespace Matches.Application.Teams.Queries.GetAllTeams
{
    public class GetAllTeamsCommandHandler : IRequestHandler<GetAllTeamsCommand, List<Team>>
    {
        private readonly ITeamRepository _teamRepository;

        public GetAllTeamsCommandHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public Task<List<Team>> Handle(GetAllTeamsCommand request, CancellationToken cancellationToken)
        {

            return _teamRepository.GetAll();



        }
    }
}
