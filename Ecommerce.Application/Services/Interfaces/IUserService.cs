using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Dtos;
using Ecommerce.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> UserExists(string userName);
        Task<UserDto> GetUserByUserName(string userName);
        Task<SignInResult> CheckUserPassWordAsync(UserDto user, string passWord);
        Task<UserDto> CreateAccountAsync(UserDto user);
        Task<UserDto> UpdateAccountAsync(UserDto user);
    }
}