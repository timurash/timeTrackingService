using AutoMapper;
using BLL.DTO;
using DAL.Entities;

namespace BLL.AutomapperProfiles
{
    public class UserDTOProfile : Profile
    {
        public UserDTOProfile()
        {
            CreateMap<User, UserDTO>();
        }
    }
}