using System;
using System.Threading.Tasks;
using Base.Domain.SeedWork;

namespace Matches.Domain.Match
{
    public interface IMatchRepository
    {
        Task AddAsync(Match match);

        Task<Match> GetAsync(MatchId id);

        Task<int> CommitAsync();
    }
}