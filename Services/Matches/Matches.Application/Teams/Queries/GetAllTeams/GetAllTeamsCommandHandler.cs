using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

namespace Matches.Application.Teams.Queries.GetAllTeams
{
    public class GetAllTeamsCommandHandler : IRequestHandler<GetAllTeamsCommand, TeamsViewModel>
    {
        //private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;

        public GetAllTeamsCommandHandler(
            //ITeamRepository teamRepository,
            IMapper mapper)
        {
            //_teamRepository = teamRepository;
            _mapper = mapper;
        }

        public async Task<TeamsViewModel> Handle(GetAllTeamsCommand request, CancellationToken cancellationToken)
        {
            //var teams = await _teamRepository.GetAllAsync();

            //var rs = _mapper.Map<List<TeamDto>>(teams);

            //var vm = new TeamsViewModel(rs);

            //return vm;
            return new TeamsViewModel(new List<TeamDto>());
        }
    }
}
