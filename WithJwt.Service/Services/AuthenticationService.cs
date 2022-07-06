using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Dtos;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WithJwt.Core.Configurations;
using WithJwt.Core.Dtos.Auths;
using WithJwt.Core.Dtos.Tokens;
using WithJwt.Core.Entites;
using WithJwt.Core.Repositories;
using WithJwt.Core.Services;
using WithJwt.Core.Services.InternalServices;
using WithJwt.Core.UnitOfWork;

namespace WithJwt.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly ITokenService _tokenService;
        private readonly List<ClientNoIdentity> _clientNoIdentities;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitofWork _unitofWork;
        private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;


        public AuthenticationService(ITokenService tokenService, UserManager<AppUser> userManager, IUnitofWork unitofWork, IUserRefreshTokenRepository userRefreshTokenRepository, IOptions<List<ClientNoIdentity>> clientNoIdentities)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _unitofWork = unitofWork;
            _userRefreshTokenRepository = userRefreshTokenRepository;
            _clientNoIdentities = clientNoIdentities.Value;
        }


        public async Task<ResponseResult<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null)
            {
                throw new ArgumentNullException(nameof(loginDto));
            }
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return ResponseResult<TokenDto>.Fail("No such user was found.", 400, true);
            }
            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return ResponseResult<TokenDto>.Fail("Wrong email or password.", 400, true);
            }
            var tokenDto = _tokenService.CreateToken(user);
            var userRefreshToken = await _userRefreshTokenRepository.Where(x => x.Id == user.Id).SingleOrDefaultAsync();
            if (userRefreshToken == null)
            {
                await _userRefreshTokenRepository.AddAsync(new UserRefreshToken
                {
                    Code = tokenDto.RefreshToken,
                    AppUserId = user.Id,
                    ExpirationDate = tokenDto.RefreshTokenExpirationDate
                });
            }
            else
            {
                userRefreshToken.Code = tokenDto.RefreshToken;
                userRefreshToken.ExpirationDate = tokenDto.RefreshTokenExpirationDate;
            }
            await _unitofWork.CommitAsync();

            return ResponseResult<TokenDto>.Success(tokenDto, 200);
        }

        public ResponseResult<ClientNoIdentityTokenDto> CreateClientNoIdentityToken(ClientLoginNoIdentityDto clientLoginNoIdentityDto)
        {
            var clientNoIdentity = _clientNoIdentities.SingleOrDefault(x => x.ClientId == clientLoginNoIdentityDto.ClientId && x.ClientSecret == clientLoginNoIdentityDto.ClientSecret);
            if (clientNoIdentity == null)
            {
                return ResponseResult<ClientNoIdentityTokenDto>.Fail("Not found client.", 404, true);
            }
            var tokenDto = _tokenService.CreateTokenByClient(clientNoIdentity);
            return ResponseResult<ClientNoIdentityTokenDto>.Success(tokenDto, 200);
        }

        public async Task<ResponseResult<TokenDto>> CreateTokenByRefreshTokenAsync(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenRepository.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();
            if (existRefreshToken == null)
            {
                return ResponseResult<TokenDto>.Fail("Not found refresh token.", 404, true);
            }
            var user = await _userManager.FindByIdAsync(existRefreshToken.AppUserId.ToString());
            if (user == null)
            {
                return ResponseResult<TokenDto>.Fail("User not found.", 404, true);
            }
            var tokenDto = _tokenService.CreateToken(user);
            existRefreshToken.Code = tokenDto.RefreshToken;
            existRefreshToken.ExpirationDate = tokenDto.RefreshTokenExpirationDate;
            await _unitofWork.CommitAsync();
            return ResponseResult<TokenDto>.Success(tokenDto, 200);
        }


        public async Task<ResponseResult<NoDataResult>> RevokeRefreshTokenAsync(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenRepository.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();
            if (existRefreshToken == null)
            {
                return ResponseResult<NoDataResult>.Fail("Not found refresh token.", 404, true);
            }
            _userRefreshTokenRepository.HardRemove(existRefreshToken);
            return ResponseResult<NoDataResult>.Success(200);

        }
    }
}
