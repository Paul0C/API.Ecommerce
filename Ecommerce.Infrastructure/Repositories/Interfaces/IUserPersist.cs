using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Identity;

namespace Ecommerce.Infrastructure.Repositories.Interfaces
{
    public interface IUserPersist : IGeralPersist
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task<User> GetUserByUserName(string userName);
    }
}