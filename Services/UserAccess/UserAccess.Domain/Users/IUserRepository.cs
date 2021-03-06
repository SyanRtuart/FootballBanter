﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAccess.Domain.Users
{
    public interface IUserRepository
    {
        Task<User> GetAsync(UserId userId);

        Task AddAsync(User user);

        Task<int> Commit();

    }
}
