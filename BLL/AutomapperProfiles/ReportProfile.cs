using System;
using AutoMapper;
using BLL.DTO;
using DAL.Entities;

namespace BLL.AutomapperProfiles
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<Report, ReportDTO>();
        }
    }
}