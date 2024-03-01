using AutoMapper;
using UserDataAccessLayer.Entities;

namespace UserDataAccessLayer.InternalServices
{
    public class UserMapper : IUserMapper
    {
        private MapperConfiguration mapperConfig;

        public IMapper InitializeMapper()
        {
            mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<InternalUser, ReqResUser>();
            });
            var _mapper = mapperConfig.CreateMapper();
            return _mapper;
        }
    }
}
