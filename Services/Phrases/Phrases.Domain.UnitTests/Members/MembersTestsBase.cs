using System;
using Phrases.Domain.Members;
using Phrases.Domain.UnitTests.SeedWork;

namespace Phrases.Domain.UnitTests.Members
{
    public class MembersTestsBase : TestBase
    {
        protected class MemberTestDataOptions
        {
            internal MemberTestDataOptions(Guid memberId, string email, string login, string firstName, string lastName,
                string name, byte[] picture)
            {
                MemberId = memberId;
                Email = email;
                Login = login;
                FirstName = firstName;
                LastName = lastName;
                Name = name;
                Picture = picture;
            }

            internal Guid MemberId { get; set; }
            internal string Email { get; set; }
            internal string Login { get; set; }
            internal string FirstName { get; set; }
            internal string LastName { get; set; }
            internal string Name { get; set; }
            internal byte[] Picture { get; set; }
        }

        protected class MemberTestData
        {
            public MemberTestData(Member member)
            {
                Member = member;
            }

            internal Member Member { get; }
        }

        protected MemberTestData CreateMemberTestData(MemberTestDataOptions options)
        {
            var member = Member.CreateNew(options.MemberId, options.Email, options.Login,
                options.FirstName, options.LastName, options.Name);

            DomainEventsTestHelper.ClearAllDomainEvents(member);

            return new MemberTestData(member);
        }
    }
}
