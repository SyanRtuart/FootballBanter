using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phrases.Application.Phrases.Queries.GetPhrasesForMatch
{
    public class PhraseDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public bool Positive { get; set; }

        public int Score { get; set; }
    }
}
