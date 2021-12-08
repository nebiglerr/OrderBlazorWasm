using MealOrdering.Server.Services.Infasture;
using MealOrdering.Shared.Dto;
using MealOrdering.Shared.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealOrdering.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService UserService)
        {
            userService = UserService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ServiceResponse<UserLoginResponseDto>> Login(UserLoginRequestDto UserRequest)
        {
            return new ServiceResponse<UserLoginResponseDto>()
            {
                Value = await userService.Login(UserRequest.Email, UserRequest.Password)
            };
        }

        [HttpGet("Users")]
        public async Task<ServiceResponse<List<UserDto>>> GetUsers()
        {
            return new ServiceResponse<List<UserDto>>()
            {
                Value = await userService.GetUsers()
            };
        }

        [HttpPost("Create")]
        public async Task<ServiceResponse<UserDto>> CreateUser([FromBody] UserDto User)
        {
            return new ServiceResponse<UserDto>()
            {
                Value = await userService.CreateUser(User)
            };
        }

        [HttpPost("Update")]
        public async Task<ServiceResponse<UserDto>> UpdateUser([FromBody] UserDto User)
        {
            return new ServiceResponse<UserDto>()
            {
                Value = await userService.UpdateUser(User)
            };
        }

        [HttpGet("UserById/{Id}")]
        public async Task<ServiceResponse<UserDto>> GetUserById(Guid Id)
        {
            return new ServiceResponse<UserDto>()
            {
                Value = await userService.GetUserById(Id)
            };
        }


        [HttpPost("Delete")]
        public async Task<ServiceResponse<bool>> DeleteUser([FromBody] Guid id)
        {
            return new ServiceResponse<bool>()
            {
                Value = await userService.DeleteUserById(id)
            };
        }
    }
}