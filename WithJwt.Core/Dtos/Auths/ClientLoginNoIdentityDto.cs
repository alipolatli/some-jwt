using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithJwt.Core.Dtos.Auths
{
    public class ClientLoginNoIdentityDto
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

    }
}
