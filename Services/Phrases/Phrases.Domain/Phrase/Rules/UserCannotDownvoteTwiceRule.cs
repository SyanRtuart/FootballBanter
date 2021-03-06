﻿using System;
using System.Collections.Generic;
using System.Linq;
using Base.Domain.SeedWork;
using Phrases.Domain.User;

namespace Phrases.Domain.Phrase.Rules
{
    public class UserCannotDownvoteTwiceRule : IBusinessRule
    {
        private readonly List<PhraseVoteHistory> _phraseVoteHistory;
        private readonly UserId _userId;

        internal UserCannotDownvoteTwiceRule(UserId userId, List<PhraseVoteHistory> phraseVoteHistory)
        {
            _userId = userId;
            _phraseVoteHistory = phraseVoteHistory;
        }

        public bool IsBroken()
        {
            return _phraseVoteHistory.SingleOrDefault(x => x.HasAlreadyDownvoted(_userId)) != null;
        }

        public string Message => "Users cannot Downvote more than once.";
    }
}
