using System;
using Matches.Application.Configuration.Queries;
using Matches.Application.Contracts;
using Matches.Application.Matches.SharedModels;
using MediatR;

namespace Matches.Application.Matches.Queries.GetMatchById
{
    public class GetMatchByIdQuery : QueryBase<MatchDto>
    {
        public GetMatchByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}