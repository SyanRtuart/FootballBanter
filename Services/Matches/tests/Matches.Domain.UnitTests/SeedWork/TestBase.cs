using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Domain;
using Base.Domain.Exceptions;
using Base.Domain.SeedWork;
using NUnit.Framework;

namespace Matches.Domain.UnitTests.SeedWork
{
    public abstract class TestBase
    {
        public static T AssertPublishedDomainEvent<T>(Entity aggregate) where T : IDomainEvent
        {
            var domainEvent = DomainEventsTestHelper.GetAllDomainEvents(aggregate).OfType<T>().SingleOrDefault();

            if (domainEvent == null)
            {
                throw new Exception($"{typeof(T).Name} event not published");
            }

            return domainEvent;
        }

        public static void AssertBrokenRule<TRule>(TestDelegate testDelegate) where TRule : class, IBusinessRule
        {
            var message = $"Expected {typeof(TRule).Name} broken rule";
            var businessRuleValidationException = Assert.Catch<BusinessRuleValidationException>(testDelegate, message);
            if (businessRuleValidationException != null)
            {
                Assert.That(businessRuleValidationException.BrokenRule, Is.TypeOf<TRule>(), message);
            }
        }
    }
}
