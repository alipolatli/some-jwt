using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithJwt.Core.Entites
{
    public class UserRefreshToken
    {

        public int Id { get; set; }

        public string Code { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int AppUserId { get; set; }

        public AppUser AppUser { get; set; }
    }
}
