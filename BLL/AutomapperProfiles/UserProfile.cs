using System;
using AutoMapper;
using BLL.DTO;
using DAL.Entities;

namespace BLL.AutomapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();
        }
    }
}