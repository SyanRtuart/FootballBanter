﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Teams.Domain.Aggregates;
using Teams.Domain.Aggregates.TeamAggregate;

namespace Matches.Application.Teams.Commands.AddTeam
{
    public class AddTeamCommandHandler : IRequestHandler<AddTeamCommand, bool>
    {
        private readonly ITeamRepository _teamRepository;

        public AddTeamCommandHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<bool> Handle(AddTeamCommand request, CancellationToken cancellationToken)
        {
            var team = new Team(request.Name);

            _teamRepository.Add(team);

            return await _teamRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
