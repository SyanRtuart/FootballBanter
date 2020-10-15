using System;
using Phrases.Domain.UnitTests.SeedWork;

namespace Phrases.Domain.UnitTests.Phrases
{
    public class PhraseTestBase : TestBase
    {
        protected class PhraseTestDataOptions
        {
            internal Guid MatchId { get; set; }
            internal Guid TeamId { get; set; }
            internal Guid CreatedByUserId { get; set; }
            internal string Description { get; set; }
        }

        protected class PhraseTestData
        {
            public PhraseTestData(Phrase.Phrase phrase)
            {
                Phrase = phrase;
            }

            internal Phrase.Phrase Phrase { get; }
        }

        protected PhraseTestData CreatePhraseTestData(PhraseTestDataOptions options)
        {
            var phrase = Phrase.Phrase.Create(options.MatchId, options.TeamId,
                options.CreatedByUserId, options.Description, true);

            DomainEventsTestHelper.ClearAllDomainEvents(phrase);

            return new PhraseTestData(phrase);
        }
    }
}
