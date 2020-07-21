using System.Collections.Generic;
using System.Threading.Tasks;

namespace Matches.Infrastructure.Configuration.Integration
{
    public interface IIntegrationService
    {
        Task<TeamsResponse> GetTeams();
    }
}
