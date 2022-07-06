using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WithJwt.Core.Dtos.Users;

namespace WithJwt.Core.Services
{
    public interface IUserService
    {
        Task<ResponseResult<AppUserDto>> RegisterUserAsync(UserCreateDto userCreateDto);

        Task<ResponseResult<AppUserDto>> GetUserByUserNameAsync(string userName);

    }
}
