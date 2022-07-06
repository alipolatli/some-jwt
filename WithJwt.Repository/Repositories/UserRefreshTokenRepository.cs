using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WithJwt.Core.Entites;
using WithJwt.Core.Repositories;
using WithJwt.Repository.Contexts;

namespace WithJwt.Repository.Repositories
{
    public class UserRefreshTokenRepository : GenericRepository<UserRefreshToken>, IUserRefreshTokenRepository
    {
        public UserRefreshTokenRepository(JwtCourseDbContext jwtCourseDbContext) : base(jwtCourseDbContext)
        {

        }
    }
}
