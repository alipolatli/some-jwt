using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithJwt.Core.Dtos.Users
{
    public class AppUserDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? City { get; set; }

        public DateTime BirthDate { get; set; }

        public int? Gender { get; set; }



    }
}
