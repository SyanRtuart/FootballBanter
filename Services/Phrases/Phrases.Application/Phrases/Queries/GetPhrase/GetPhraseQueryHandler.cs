using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Base.Infrastructure;
using Dapper;
using Phrases.Application.Configuration.Queries;

namespace Phrases.Application.Phrases.Queries.GetPhrase
{
    public class GetPhraseQueryHandler : IQueryHandler<GetPhraseQuery, PhraseDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetPhraseQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<PhraseDto> Handle(GetPhraseQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT * " +
                               "FROM Phrase.Phrases AS p " +
                               "WHERE DateDeleted IS NULL AND " +
                               "p.Id = @PhraseId";

            var phrases = await connection.QueryAsync<PhraseDto>(sql, new
            {
                request.PhraseId
            });

            return phrases.FirstOrDefault();
        }
    }
}
