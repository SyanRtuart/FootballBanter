using System;
using Base.Domain;

namespace UserAccess.Domain.UserRegistrations.Events
{
    public class NewUserRegisteredDomainEvent : DomainEventBase
    {
        public NewUserRegisteredDomainEvent(
            Guid userRegistrationId,
            string login, string email, string firstName, string lastName, string name, DateTime registerDate)
        {
            UserRegistrationId = userRegistrationId;
            Login = login;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Name = name;
            RegisterDate = registerDate;
        }

        public Guid UserRegistrationId { get; }

        public string Login { get; }

        public string Email { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Name { get; }

        public DateTime RegisterDate { get; }
    }
}