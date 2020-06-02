using System;
using MediatR;

namespace Matches.Application.Teams.Commands.CreateTeam
{
    public class CreateTeamCommand : IRequest<Guid>
    {
        public CreateTeamCommand(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}