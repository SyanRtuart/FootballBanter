using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phrases.Application.Phrases.Queries.GetPhrase
{
    public class PhraseDto
    {
        public Guid Id { get; set; }
        public Guid MatchId { get; set; }
        public Guid TeamId { get; set; }
        public Guid CreatedByUserId { get; set; }
        public string Description { get; set; }
        public bool Positive { get; set; }
        public int Score { get; set; }
    }
}
