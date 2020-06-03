using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Domain.SeedWork;

namespace UserAccess.Domain.UserRegistrations
{
    public class UserRegistration : Entity, IAggregateRoot
    {
        private string _login;

        private string _password;

        private string _email;

        private string _firstName;

        private string _lastName;

        private string _name;

        private DateTime _registerDate;

        private UserRegistrationStatus _status;

        private DateTime? _confirmedDate;

        private UserRegistration()
        {
            // Only EF.
        }

        public static UserRegistration RegisterNewUser(
            string login,
            string password,
            string email,
            string firstName,
            string lastName,
            IUsersCounter usersCounter)
        {
            return new UserRegistration(login, password, email, firstName, lastName, usersCounter);
        }

        private UserRegistration(
            string login,
            string password,
            string email,
            string firstName,
            string lastName,
            IUsersCounter usersCounter)
        {
            //this.CheckRule(new UserLoginMustBeUniqueRule(usersCounter, login));

            _login = login;
            _password = password;
            _email = email;
            _firstName = firstName;
            _lastName = lastName;
            _name = $"{firstName} {lastName}";
            _registerDate = DateTime.UtcNow;
            _status = UserRegistrationStatus.WaitingForConfirmation;
            
            //this.AddDomainEvent(new NewUserRegisteredDomainEvent(this.Id, _login, _email, _firstName, _lastName, _name, _registerDate));
        }

        public void Confirm()
        {
            //this.CheckRule(new UserRegistrationCannotBeConfirmedMoreThanOnceRule(_status));
            //this.CheckRule(new UserRegistrationCannotBeConfirmedAfterExpirationRule(_status));

            _status = UserRegistrationStatus.Confirmed;
            _confirmedDate = DateTime.UtcNow;

            //this.AddDomainEvent(new UserRegistrationConfirmedDomainEvent(this.Id));
        }
    }
}
