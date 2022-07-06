using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithJwt.Service.Mappers.AutoMapper
{
    public static class ObjectMapper
    {
        private static Lazy<IMapper> lazyMapper = new Lazy<IMapper>(() =>
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(configure =>
            {
                configure.AddProfile(typeof(DtoProfile));
            });
            return mapperConfiguration.CreateMapper();
        });

        public static IMapper Mapper => lazyMapper.Value;
    }
}
