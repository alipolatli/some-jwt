using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithJwt.Core.Entites
{
    public class Deneme
    {
        public int Id { get; set; }
        
        public string Abc { get; set; }

        public UserRefreshToken UserRefreshToken { get; set; }

    }
}
