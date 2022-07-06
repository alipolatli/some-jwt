using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithJwt.Core.Dtos.Tokens
{
    //bu dto'nun access token payloudunda üyelik sistemi (user ıd,email, rol vb.) veriler olmayacak ve ilgili üyelik sistemi gerektirmeyen  apilere yalnızca access token paylodunda bulunan  public string ClientId { get; set; }  public string ClientSecret { get; set; } ile istek yapacak.

    public class ClientNoIdentityTokenDto
    {
        public string AccessToken { get; set; }
        
        //payloudunda zaten var frontendçiye dönmek için.
        public DateTime AccessTokenExpirationDate { get; set; }
    }
}
