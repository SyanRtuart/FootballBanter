using MediatR;

namespace Matches.Application.Teams.Commands.CreateTeam
{
    public class CreateTeamCommand : IRequest<bool>
    {
        public CreateTeamCommand(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}