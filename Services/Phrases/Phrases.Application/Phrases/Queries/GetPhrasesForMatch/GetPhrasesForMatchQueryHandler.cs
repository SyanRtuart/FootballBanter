﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Base.Infrastructure;
using Dapper;
using MediatR;

namespace Phrases.Application.Phrases.Queries.GetPhrasesForMatch
{
    public class GetPhrasesForMatchQueryHandler : IRequestHandler<GetPhrasesForMatchQuery, List<PhraseDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetPhrasesForMatchQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<PhraseDto>> Handle(GetPhrasesForMatchQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT * " +
                               "FROM Phrase.Phrases AS p " +
                               "WHERE p.MatchId = @MatchId";
            
            var phrases = await connection.QueryAsync<PhraseDto>(sql, new
            {
                request.MatchId
            });

            return phrases.AsList();
        }
    }
}
