using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phrases.Application.Phrases.Commands.CreatePhrase
{
    public class CreatePhraseRequest
    {
        public int TeamId { get; set; }

        public int MatchId { get; set; }

        public string Description { get; set; }

        public bool Positive { get; set; }
    }
}
