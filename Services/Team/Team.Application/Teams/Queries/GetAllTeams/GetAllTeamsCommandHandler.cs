using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Teams.Domain.Aggregates;

namespace Teams.Application.Teams.Queries.GetAllTeams
{
    public class GetAllTeamsCommandHandler : IRequestHandler<GetAllTeamsCommand, TeamsViewModel>
    {
        private readonly ITeamRepository _teamRepository;

        public GetAllTeamsCommandHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public Task<TeamsViewModel> Handle(GetAllTeamsCommand request, CancellationToken cancellationToken)
        {
            var vm = new TeamsViewModel();
            return null;
            //_teamRepository.g
        }
    }
}
