using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WithJwt.Core.Dtos.Posts;
using WithJwt.Core.Dtos.Users;
using WithJwt.Core.Entites;

namespace WithJwt.Service.Mappers.AutoMapper
{
    public class DtoProfile : Profile
    {
        public DtoProfile()
        {
            CreateMap<Post, PostDto>().ReverseMap();
            CreateMap<Post, PostAddDto>().ReverseMap();
            CreateMap<Post, PostUpdateDto>().ReverseMap();
            CreateMap<Post, PostListDto>().ReverseMap();
            CreateMap<AppUser, AppUserDto>().ReverseMap();
            CreateMap<AppUser, UserCreateDto>().ReverseMap();
        }
    }
}
