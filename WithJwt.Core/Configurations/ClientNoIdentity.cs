using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithJwt.Core.Configurations
{
    //üyelik gerektirmeyen api'lerde kullanmak için tasarlandı. bu sınıf iç mekanizma içindir dto'su olusturulacaktır.(ClientLoginNoIdentityDto)
    public class ClientNoIdentity
    {
        public string ClientId { get; set; }
        
        public string ClientSecret { get; set; }

        //wwww.myapi1.com www.myapi2.com
        public List<string> Audiences { get; set; }

    }
}
