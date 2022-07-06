using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithJwt.Core.Dtos.Posts
{
    public class PostListDto:PaginationDto
    {
        public IEnumerable<PostDto> PostDtos { get; set; }
    }
}
