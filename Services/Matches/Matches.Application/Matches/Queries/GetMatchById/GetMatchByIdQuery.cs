using System;
using Matches.Application.Matches.SharedModels;
using MediatR;

namespace Matches.Application.Matches.Queries.GetMatchById
{
    public class GetMatchByIdQuery : IRequest<MatchDto>
    {
        public GetMatchByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}