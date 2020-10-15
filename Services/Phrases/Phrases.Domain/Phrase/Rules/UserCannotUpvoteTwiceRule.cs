using System;
using System.Collections.Generic;
using System.Linq;
using Base.Domain.SeedWork;
using Phrases.Domain.User;

namespace Phrases.Domain.Phrase.Rules
{
    public class UserCannotUpvoteTwiceRule : IBusinessRule
    {
        private readonly List<PhraseVoteHistory> _phraseVoteHistory;
        private readonly UserId _userId;

        internal UserCannotUpvoteTwiceRule(UserId userId, List<PhraseVoteHistory> phraseVoteHistory)
        {
            _userId = userId;
            _phraseVoteHistory = phraseVoteHistory;
        }

        public bool IsBroken()
        {
            return _phraseVoteHistory.SingleOrDefault(x => x.HasAlreadyUpvoted(_userId)) != null;
        }

        public string Message => "Users cannot Upvote more than once.";
    }
}
