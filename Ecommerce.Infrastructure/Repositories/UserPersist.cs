using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Identity;
using Ecommerce.Infrastructure.EntityFramework.Contextos;
using Ecommerce.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories
{
    public class UserPersist : GeralPersist, IUserPersist
    {
        private readonly DataContext _dataContext;
        public UserPersist(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _dataContext.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            return await _dataContext.Users.FindAsync(userName);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _dataContext.Users.ToListAsync();
        }
    }
}