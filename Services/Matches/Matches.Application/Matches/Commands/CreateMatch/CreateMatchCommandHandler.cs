using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Matches.Domain.Aggregates.MatchAggregate.;
using MediatR;
using Matches.Domain.Aggregates.MatchAggregate;

namespace Matches.Application.Matches.Commands.CreateMatch
{
    public class CreateMatchCommandHandler : IRequestHandler<CreateMatchCommand, Unit>
    {
        private readonly IMatchRepository _matchRepository;

        public CreateMatchCommandHandler(IMatchRepository matchRepository)
        {
            matchRepository = _matchRepository;
        }

        public Task<Unit> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
        {

            //TODO
            // var status = await _matchRepository;
            //Match.Create();
        }
    }
}
