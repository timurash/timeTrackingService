using AutoMapper;
using BLL.DTO;
using PL.Models;

namespace PL.AutoMapperProfiles
{
    public class UserViewProfile : Profile
    {
        public UserViewProfile()
        {
            CreateMap<UserDTO, UserViewModel>();
        }
    }
}