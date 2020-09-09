using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserAccess.Domain.Users;
using UserAccess.Infrastructure.Persistence;

namespace UserAccess.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserAccessContext _context;

        public UserRepository(UserAccessContext context)
        {
            _context = context;
        }

        public async Task<User> GetAsync(Guid userId)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }
    }
}
