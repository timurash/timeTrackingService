using AutoMapper;
using BLL.DTO;
using DAL.Entities;

namespace BLL.AutoMapperProfiles
{
    public class ReportDTOProfile : Profile
    {
        public ReportDTOProfile()
        {
            CreateMap<Report, ReportDTO>();
        }
    }
}