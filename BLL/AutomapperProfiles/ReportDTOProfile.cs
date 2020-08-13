using AutoMapper;
using BLL.DTO;
using DAL.Entities;

namespace BLL.AutomapperProfiles
{
    public class ReportDTOProfile : Profile
    {
        public ReportDTOProfile()
        {
            CreateMap<Report, ReportDTO>();
        }
    }
}