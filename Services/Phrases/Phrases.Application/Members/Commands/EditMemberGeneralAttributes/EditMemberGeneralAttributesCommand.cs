using System;
using Newtonsoft.Json;
using Phrases.Application.Contracts;

namespace Phrases.Application.Members.Commands.EditMemberGeneralAttributes
{
    public class EditMemberGeneralAttributesCommand : CommandBase
    {
        public EditMemberGeneralAttributesCommand(Guid memberId, string firstName, string lastName, byte[] picture)
        {
            MemberId = memberId;
            FirstName = firstName;
            LastName = lastName;
            Picture = picture;
        }
        [JsonIgnore]
        public Guid MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Picture { get; set; }

        public EditMemberGeneralAttributesCommand()
        {
            // only for open api
        }
    }
}
