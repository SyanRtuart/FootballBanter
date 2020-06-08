using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserAccess.Domain.UserRegistrations;
using UserAccess.Infrastructure.Persistence;

namespace UserAccess.Infrastructure.Repositories
{
    public class UserRegistrationRepository : IUserRegistrationRepository
    {
        private readonly UserAccessContext _userAccessContext;

        public UserRegistrationRepository(UserAccessContext userAccessContext)
        {
            _userAccessContext = userAccessContext;
        }

        public async Task AddAsync(UserRegistration userRegistration)
        {
            await _userAccessContext.AddAsync(userRegistration);
        }

        public async Task<UserRegistration> GetByIdAsync(Guid userRegistrationId)
        {
            return await _userAccessContext.UserRegistrations.FirstOrDefaultAsync(x => x.Id == userRegistrationId);
        }
    }
}