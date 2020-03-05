namespace Matches.Application.Teams.Commands.AddTeam
{
    public class AddTeamCommand : IRequest<bool>
    {
        public string Name { get; private set; }

        public AddTeamCommand(string name)
        {
            Name = name;
        }
    }
}
