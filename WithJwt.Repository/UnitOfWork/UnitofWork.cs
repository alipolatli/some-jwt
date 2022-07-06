using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WithJwt.Core.UnitOfWork;
using WithJwt.Repository.Contexts;

namespace WithJwt.Repository.UnitOfWork
{
    public class UnitofWork : IUnitofWork
    {
        private readonly JwtCourseDbContext _jwtCourseDbContext;

        public UnitofWork(JwtCourseDbContext jwtCourseDbContext)
        {
            _jwtCourseDbContext = jwtCourseDbContext;
        }

        public async Task CommitAsync()
        {
            await _jwtCourseDbContext.SaveChangesAsync();
        }
    }
}
