using System.Threading.Tasks;

namespace Phrases.Domain.Members
{
    public interface IMemberRepository
    {
        Task AddAsync(Member member);
    }
}
