using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WithJwt.Core.Configurations;
using WithJwt.Core.Dtos.Tokens;
using WithJwt.Core.Entites;

namespace WithJwt.Core.Services.InternalServices
{
    public interface ITokenService
    {
        TokenDto CreateToken(AppUser appUser);

        ClientNoIdentityTokenDto CreateTokenByClient(ClientNoIdentity client);
    }
}
