using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Application.Dtos;
using Ecommerce.Application.Services.Interfaces;
using Ecommerce.Domain.Identity;
using Ecommerce.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.Services
{
    public class UserService : IUserService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IUserPersist _userPersist;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager,
                           SignInManager<User> signInManager,
                           IMapper mapper,
                           IUserPersist userPersist)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userPersist = userPersist;
            _mapper = mapper;
        }

        public async Task<SignInResult> CheckUserPassWordAsync(UserDto userDto, string passWord)
        {
            try
            {
                var user = _userManager.Users.SingleOrDefault(u => u.UserName == userDto.UserName.ToLower());

                return await _signInManager.CheckPasswordSignInAsync(user, passWord, false);
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao verificar PassWord. Erro: {e.Message}");
            }
        }

        public async Task<UserDto> CreateAccountAsync(UserDto userDto)
        {
            try
            {
               var user = _mapper.Map<User>(userDto);
               var result = await _userManager.CreateAsync(user, userDto.PassWord);
               

               if(result.Succeeded){
                var userRetorno = _mapper.Map<UserDto>(user);
                return userRetorno;
               }

                return null;
            }
            catch (Exception e)
            {
                
                throw new Exception($"Erro ao criar usuário. Erro: {e.Message}");
            }
        }

        public async Task<UserDto> GetUserByUserName(string userName)
        {
            try
            {
                var user = await _userPersist.GetUserByUserName(userName);
                if(user == null) return null;

                var userRetorno = _mapper.Map<UserDto>(user);

                return userRetorno;
            }
            catch (Exception e)
            {
                
                throw new Exception($"Erro ao recuperar usuário por userName. Erro: {e.Message}");
            }
        }

        public async Task<UserDto> UpdateAccountAsync(UserDto userDto)
        {
            try
            {
                var userBanco = await _userPersist.GetUserByUserName(userDto.UserName);
                if(userBanco == null) return null;

                var user = _mapper.Map<User>(userDto);

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, userDto.PassWord);

                _userPersist.Update<User>(user);

                var userAtualizado = await _userPersist.GetUserById(user.Id);

                var userParaRetorno = _mapper.Map<UserDto>(userAtualizado);

                return userParaRetorno;

            }
            catch (Exception e)
            {
                
                throw new Exception($"Erro ao atualizar Usuário. Erro: {e.Message}");
            }
        }

        public async Task<bool> UserExists(string userName)
        {
            try
            {
                return await _userManager.Users.AnyAsync(user => user.UserName == userName.ToLower());
            }
            catch (Exception e)
            {
                
                throw new Exception($"Erro ao verificar se o Usuário existe. Erro: {e.Message}");
            }
        }
    }
}