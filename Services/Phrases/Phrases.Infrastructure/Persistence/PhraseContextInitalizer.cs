using System.Linq;
using System.Threading;

namespace Phrases.Infrastructure.Persistence
{
    public class PhraseContextInitalizer
    {
        private readonly PhraseContext _context;

        public PhraseContextInitalizer(PhraseContext context)
        {
            _context = context;
        }

        public static void Initialize(PhraseContext context)
        {
            var initilizer = new PhraseContextInitalizer(context);
            initilizer.SeedEverything();
        }

        private async void SeedEverything()
        {
            if (!_context.Phrases.Any())
            {
                SeedPhrases();

            }
        }

        private void SeedPhrases()
        {
            //var phrases = new List<Phrase>();

            //for (int i = 0; i < 10; i++)
            //{
            //    var newPhrases = new List<Phrase>
            //    {
            //        Phrase.Create(i,i, "Good Banter 1", true),
            //        Phrase.Create(i,i, "Good Banter 2", true),
            //        Phrase.Create(i,i, "Bad Banter 1", false),
            //        Phrase.Create(i,i, "Bad Banter 1", false),


            //    };

            //    phrases.AddRange(newPhrases);
            //}

            //_context.Phrases.AddRange(phrases);
        }
    }
}