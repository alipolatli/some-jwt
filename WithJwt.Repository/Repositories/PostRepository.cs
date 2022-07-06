using Shared.Dtos;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WithJwt.Core.Dtos.Posts;
using WithJwt.Core.Entites;
using WithJwt.Core.Repositories;
using WithJwt.Core.Services;
using WithJwt.Repository.Contexts;

namespace WithJwt.Repository.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(JwtCourseDbContext jwtCourseDbContext) : base(jwtCourseDbContext)
        {

        }
    }
}
