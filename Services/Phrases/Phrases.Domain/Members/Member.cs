using System;
using Base.Domain.SeedWork;
using Phrases.Domain.Members.Events;

namespace Phrases.Domain.Members
{
    public class Member : Entity, IAggregateRoot
    {
        public MemberId Id { get; private set; }

        private string _email;

        private string _login;

        private string _firstName;

        private string _lastName;

        private string _name;

        private DateTime _createdDate;

        private Scores _scores;

        private byte[] _picture;
        
        private Member(Guid id, string email, string login, string firstName, string lastName, string name)
        {
            Id = new MemberId(id);
            _email = email;
            _login = login;
            _firstName = firstName;
            _lastName = lastName;
            _name = name;
            _createdDate = DateTime.UtcNow;

            AddDomainEvent(new MemberCreatedDomainEvent(Id));
        }
        private Member()
        {
            // Required for EF.
        }

        public static Member CreateNew(Guid id, string email, string login,  string firstName, string lastName, string name)
        {
            return new Member(id, email, login, firstName, lastName, name);
        }

        public void EditGeneralAttributes(string firstName, string lastName, byte[] picture)
        {
            _firstName = firstName;
            _lastName = lastName;
            _picture = picture;

            AddDomainEvent(new MemberGeneralAttributesEditedDomainEvent(firstName, lastName, picture));
        }
        
    }
}
