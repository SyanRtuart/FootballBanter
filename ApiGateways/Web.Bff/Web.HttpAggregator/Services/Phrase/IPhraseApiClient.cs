using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.HttpAggregator.Models.Phrase;

namespace Web.HttpAggregator.Services.Phrase
{
    public interface IPhraseApiClient
    {
        Task<List<PhraseData>> GetPhrasesAsync(int teamId, int matchId);

        Task CreatePhrase(int matchId, string description, bool positive);
    }
}
