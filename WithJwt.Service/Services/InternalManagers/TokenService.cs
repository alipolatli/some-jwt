using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared;
using Shared.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WithJwt.Core.Configurations;
using WithJwt.Core.Dtos.Tokens;
using WithJwt.Core.Entites;
using WithJwt.Core.Services.InternalServices;

namespace WithJwt.Service.Services.InternalManagers
{
    public class TokenService : ITokenService
    {

        private readonly CustomTokenOptions _customTokenOptions;

        public TokenService(IOptions<CustomTokenOptions> customTokenOptions)
        {
            _customTokenOptions = customTokenOptions.Value;
        }

        public TokenDto CreateToken(AppUser appUser)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_customTokenOptions.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_customTokenOptions.RefreshTokenExpiration);
            var securityKey = SecurityKeyService.GetSymmetricSecurityKey(_customTokenOptions.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _customTokenOptions.Issuer,
                claims: GetClaims(appUser, _customTokenOptions.Auidence),
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials
                );
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            string accessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

            return new TokenDto
            {
                AccessToken = accessToken,
                AccessTokenExpirationDate = accessTokenExpiration,
                RefreshToken = RefreshTokenStringCode(),
                RefreshTokenExpirationDate = refreshTokenExpiration,
            };
        }

        public ClientNoIdentityTokenDto CreateTokenByClient(ClientNoIdentity client)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_customTokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyService.GetSymmetricSecurityKey(_customTokenOptions.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _customTokenOptions.Issuer,
                claims: GetClaimsForClientNoIdentity(client),
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials
                );
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            string accessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

            return new ClientNoIdentityTokenDto
            {
                AccessToken = accessToken,
                AccessTokenExpirationDate = accessTokenExpiration,
            };
        }


        private string RefreshTokenStringCode()
        {
            var numberByte = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(numberByte);
            }
            return Convert.ToBase64String(numberByte);
        }

        private IEnumerable<Claim> GetClaims(AppUser appUser, List<string> audiences)
        {
            List<Claim> usersClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,appUser.Email),
                new Claim(ClaimTypes.Name,appUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };
            usersClaims.AddRange(audiences.Select(audience => new Claim(JwtRegisteredClaimNames.Aud, audience)));
            return usersClaims;
        }

        private IEnumerable<Claim> GetClaimsForClientNoIdentity(ClientNoIdentity clientNoIdentity)
        {
            List<Claim> clientsClaimsNoIdentity = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub,clientNoIdentity.ClientId),
            };
            clientsClaimsNoIdentity.AddRange(clientNoIdentity.Audiences.Select(auidence => new Claim(JwtRegisteredClaimNames.Aud, auidence)));
            return clientsClaimsNoIdentity;
        }
    }
}
