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

        public async Task<SignInResult> CheckUserPassWordAsync(UserUpdateDto userUpdateDto, string passWord)
        {
            try
            {
                var user = _userManager.Users.SingleOrDefault(u => u.UserName == userUpdateDto.UserName.ToLower());

                return await _signInManager.CheckPasswordSignInAsync(user, passWord, false);
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao verificar PassWord. Erro: {e.Message}");
            }
        }

        public async Task<UserUpdateDto> CreateAccountAsync(UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                user.DataCadastro = DateTime.Now;

                var result = await _userManager.CreateAsync(user, userDto.PassWord);
                

                if(result.Succeeded){
                    var userRetorno = _mapper.Map<UserUpdateDto>(user);
                    return userRetorno;
                }

                return null;
            }
            catch (Exception e)
            {
                
                throw new Exception($"Erro ao criar usuário. Erro: {e.Message}");
            }
        }

        public async Task<UserUpdateDto> GetUserByUserNameAsync(string userName)
        {
            try
            {
                var user = await _userPersist.GetUserByUserNameAsync(userName);
                if(user == null) return null;

                var userRetorno = _mapper.Map<UserUpdateDto>(user);
                userRetorno.DataCadastro = user.DataCadastro.ToString();
                return userRetorno;
            }
            catch (Exception e)
            {
                
                throw new Exception($"Erro ao recuperar usuário por userName. Erro: {e.Message}");
            }
        }

        public async Task<UserUpdateDto> UpdateAccountAsync(UserUpdateDto userUpdateDto)
        {
            try
            {
                var userBanco = await _userPersist.GetUserByUserNameAsync(userUpdateDto.UserName);
                if(userBanco == null) return null;

                _mapper.Map(userUpdateDto, userBanco);

                var token = await _userManager.GeneratePasswordResetTokenAsync(userBanco);
                var result = await _userManager.ResetPasswordAsync(userBanco, token, userUpdateDto.PassWord);

                _userPersist.Update<User>(userBanco);

                if(await _userPersist.SaveChangesAsync())
                {
                    var userAtualizado = await _userPersist.GetUserByIdAsync(userBanco.Id);
                    var userParaRetorno = _mapper.Map<UserUpdateDto>(userAtualizado);

                    return userParaRetorno;
                }

                
                return null;

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