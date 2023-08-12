using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.API.Extensions;
using Ecommerce.Application.Dtos;
using Ecommerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService,
                              ITokenService tokenService,
                              IMapper mapper)
        {
            _userService = userService;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser(){
            try
            {
                var userName = User.GetUsername();
                var user = await _userService.GetUserByUserNameAsync(userName);

                return user != null ?  Ok(user)
                                    : BadRequest();
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                                        $"Não foi possível recuperar o Usuário {e.Message}");
            }
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]UserDto userDto){
            try
            {
                if(await _userService.UserExists(userDto.UserName)) return BadRequest("Usuário já existe.");

                var user = await _userService.CreateAccountAsync(userDto);

                var token =  _tokenService.CreateToken(user).Result;

                return Ok(new {
                    userName = userDto.UserName,
                    token = token
                });              
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                        $"Erro ao fazer o Registro do Usuário. Erro: {e.Message}");
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login (UserLoginDto userLoginDto){
            try
            {
                var user = await _userService.GetUserByUserNameAsync(userLoginDto.UserName);
                if(user == null) return Unauthorized("Username está incorreto.");

                var result = await _userService.CheckUserPassWordAsync(user, userLoginDto.PassWord);
                if(result.Succeeded){
                    return Ok(new {
                        token = _tokenService.CreateToken(user).Result
                    }); 
                }

                return Unauthorized();
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                        $"Erro ao fazer o Login do Usuário. Erro: {e.Message}");
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> Update([FromBody] UserUpdateDto userUpdateDto){
            try
            {
                var user = await _userService.GetUserByUserNameAsync(User.GetUsername());
                if(user == null) return Unauthorized("Usuário inválido.");

                var userAtualizado = await _userService.UpdateAccountAsync(userUpdateDto);
                
                return userAtualizado != null ? Ok(userAtualizado)
                                              : NoContent();
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                        $"Erro ao atualizar Usuário. Erro: {e.Message}");
            }
        }
    }
}