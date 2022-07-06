using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithJwt.Core.Entites
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? City { get; set; }

        public DateTime? BirthDate { get; set; }

        public string? MemberDate { get; set; }

        public int? Gender { get; set; }

        public IList<Post> Posts { get; set; }

        public UserRefreshToken UserRefreshToken { get; set; }

    }
}
