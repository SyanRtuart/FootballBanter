using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Base.Infrastructure;
using Dapper;
using Phrases.Application.Configuration.Queries;

namespace Phrases.Application.Members.Queries.GetMember
{
    public class GetMemberQueryHandler : IQueryHandler<GetMemberQuery, MemberDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMemberQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<MemberDto> Handle(GetMemberQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = @"SELECT Email,
                                        FirstName,
                                        LastName,
                                        Picture,
                                        BanterScore,
                                        CommentScore
                                 FROM   Phrase.Members
                                 WHERE  ( Id = @MemberId )";

            var member = await connection.QueryAsync<MemberDto>(sql, new
            {
                request.MemberId
            });

            return member.FirstOrDefault();
        }
    }
}
