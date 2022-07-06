using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WithJwt.Core.Dtos.Users;
using WithJwt.Core.Entites;
using WithJwt.Core.Services;

namespace WithJwt.Service.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly IMapper _mapper;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<ResponseResult<AppUserDto>> RegisterUserAsync(UserCreateDto userCreateDto)
        {
            var registerUser = _mapper.Map<AppUser>(userCreateDto);
            var identityResult = await _userManager.CreateAsync(registerUser, userCreateDto.Password);
            if (identityResult.Succeeded)
            {
                return ResponseResult<AppUserDto>.Success(_mapper.Map<AppUserDto>(registerUser), 201);
            }
            else
            {
                var errors = identityResult.Errors.Select(e => e.Description).ToList();
                return ResponseResult<AppUserDto>.Fail(new ErrorResult(errors, true), 400);
            }
        }

        public async Task<ResponseResult<AppUserDto>> GetUserByUserNameAsync(string userName)
        {

            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                return ResponseResult<AppUserDto>.Success(_mapper.Map<AppUserDto>(user), 200);
            }
            return ResponseResult<AppUserDto>.Fail("Not found user", 200, false);

        }


    }
}
