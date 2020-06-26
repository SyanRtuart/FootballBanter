using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserAccess.Domain.UserRegistrations;
using UserAccess.Infrastructure.Persistence;

namespace UserAccess.Infrastructure.Repositories
{
    public class UserRegistrationRepository : IUserRegistrationRepository
    {
        private readonly UserAccessContext _context;

        public UserRegistrationRepository(UserAccessContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(UserRegistration userRegistration)
        {
            await _context.AddAsync(userRegistration);
        }

        public async Task<UserRegistration> GetByIdAsync(Guid userRegistrationId)
        {
            return await _context.UserRegistrations.FirstOrDefaultAsync(x => x.Id == userRegistrationId);
        }
    }
}