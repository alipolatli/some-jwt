using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithJwt.Core.Dtos.Tokens
{
    //bu dto'nun access token payloudunda üyelik sistemi (user ıd,email, rol vb.) veriler bulunacak ve ilgili apilere bu veilerle istek yapılacak.
    public class TokenDto
    {
        public string AccessToken { get; set; }

        //payloud'unda zaten var verilmeyebilir. fakat frontendçiye göndermek amacıyla yazıldı. 
        public DateTime AccessTokenExpirationDate { get; set; }

        public string RefreshToken { get; set; }

        //payloud'unda zaten var verilmeyebilir. fakat frontendçiye göndermek amacıyla yazıldı. 
        public DateTime RefreshTokenExpirationDate { get; set; }


    }
}
