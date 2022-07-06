using Shared.Dtos;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WithJwt.Core.Dtos.Auths;
using WithJwt.Core.Dtos.Tokens;

namespace WithJwt.Core.Services
{
    public interface IAuthenticationService
    {
        Task<ResponseResult<TokenDto>> CreateTokenAsync(LoginDto loginDto);
        Task<ResponseResult<TokenDto>> CreateTokenByRefreshTokenAsync(string refreshToken);
        Task<ResponseResult<NoDataResult>> RevokeRefreshTokenAsync(string refreshToken);

        ResponseResult<ClientNoIdentityTokenDto> CreateClientNoIdentityToken(ClientLoginNoIdentityDto clientLoginNoIdentityDto);



    }
}
