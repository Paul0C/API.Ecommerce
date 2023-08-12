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

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _dataContext.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            var userNameParametro = userName.ToLower();
            return await _dataContext.Users.FirstAsync(user => user.UserName == userNameParametro);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _dataContext.Users.ToListAsync();
        }
    }
}