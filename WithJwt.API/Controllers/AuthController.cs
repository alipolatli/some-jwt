using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WithJwt.Core.Dtos.Auths;
using WithJwt.Core.Dtos.Tokens;
using WithJwt.Core.Dtos.Users;
using WithJwt.Core.Services;

namespace WithJwt.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : BaseController
    {

        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken(LoginDto loginDto)
        {
            var result = await _authenticationService.CreateTokenAsync(loginDto);
            return ActionResultInstance<TokenDto>(result);
        }

        [HttpPost]
        public IActionResult CreateTokenForNoIdentityClient(ClientLoginNoIdentityDto clientLoginNoIdentityDto)
        {
            var result = _authenticationService.CreateClientNoIdentityToken(clientLoginNoIdentityDto);
            return ActionResultInstance<ClientNoIdentityTokenDto>(result);
        }
        [HttpPost]
        public async Task<IActionResult> RevokeRefreshToken(string refreshToken)
        {
            var result = await _authenticationService.RevokeRefreshTokenAsync(refreshToken);
            return ActionResultInstance(result);

        }

        [HttpPost]
        public async Task<IActionResult> CreateTokenByRefreshToken(string refreshToken)
        {
            var result = await _authenticationService.CreateTokenByRefreshTokenAsync(refreshToken);
            return ActionResultInstance(result);
        }

    }
}
