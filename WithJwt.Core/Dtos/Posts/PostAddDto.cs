using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithJwt.Core.Dtos.Posts
{
    public class PostAddDto
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public int AppUserId { get; set; }

        //public DateTime UpdatedDate { get; set; } = DateTime.Now;


    }
}
