using System;
using System.Security.AccessControl;
using Newtonsoft.Json;
using Phrases.Application.Configuration.Commands;

namespace Phrases.Application.Members.Commands.CreateMember
{
    public class CreateMemberCommand : InternalCommandBase
    {
        [JsonConstructor]
        internal CreateMemberCommand(Guid id, Guid memberId, string login, string email, string firstName, string lastName, string name) : base(id)
        {
            MemberId = memberId;
            Login = login;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Name = name;
        }
        internal Guid MemberId { get; }
        
        internal string Login { get; }

        internal string Email { get; }

        internal string FirstName { get; }

        internal string LastName { get; }

        internal string Name { get; }
    }
}
