using AutoMapper;
using RssReaders.Core.Model;
using RssReaders.Infrastructure.DTO;

namespace RssReaders.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize() => new MapperConfiguration (cfg => {
            cfg.CreateMap<User, AccountDTO>();
        }).CreateMapper();
    }
}