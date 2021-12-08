using MealOrdering.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealOrdering.Server.Services.Infasture
{
    public interface IUserService
    {
        public Task<UserDto> GetUserById(Guid Id);

        public Task<List<UserDto>> GetUsers();

        public Task<UserDto> CreateUser(UserDto User);

        public Task<UserDto> UpdateUser(UserDto User);

        public Task<bool> DeleteUserById(Guid Id);

       public Task<UserLoginResponseDto> Login(string EMail, string Password);
    }
}
