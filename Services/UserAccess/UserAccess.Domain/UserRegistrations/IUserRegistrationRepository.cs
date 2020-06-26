using System;
using System.Threading.Tasks;

namespace UserAccess.Domain.UserRegistrations
{
    public interface IUserRegistrationRepository
    {
        Task AddAsync(UserRegistration userRegistration);

        Task<UserRegistration> GetByIdAsync(Guid userRegistrationId);
    }
}