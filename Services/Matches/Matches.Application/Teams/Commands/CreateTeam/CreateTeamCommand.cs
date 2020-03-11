using MediatR;

namespace Matches.Application.Teams.Commands.CreateTeam
{
    public class CreateTeamCommand : IRequest<bool>
    {
        public string Name { get; }

        public CreateTeamCommand(string name)
        {
            Name = name;
        }
    }
}
