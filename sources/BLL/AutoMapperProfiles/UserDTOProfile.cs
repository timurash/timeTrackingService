using AutoMapper;
using BLL.DTO;
using DAL.Entities;

namespace BLL.AutoMapperProfiles
{
    public class UserDTOProfile : Profile
    {
        public UserDTOProfile()
        {
            CreateMap<User, UserDTO>();
        }
    }
}