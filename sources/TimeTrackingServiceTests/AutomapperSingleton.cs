using AutoMapper;
using PL.AutoMapperProfiles;
using BLL.AutoMapperProfiles;

namespace TimeTrackingService.Tests
{
    public class AutomapperSingleton
    {
        private static IMapper _mapper;

        public static IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    // Auto Mapper Configurations
                    var mappingConfig = new MapperConfiguration(mc =>
                    {
                        mc.AddProfile(new UserDTOProfile());
                        mc.AddProfile(new ReportDTOProfile());
                        mc.AddProfile(new UserViewProfile());
                        mc.AddProfile(new ReportViewProfile());
                    });
                    IMapper mapper = mappingConfig.CreateMapper();
                    _mapper = mapper;
                }
                return _mapper;
            }
        }
    }
}