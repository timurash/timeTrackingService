using AutoMapper;
using BLL.DTO;
using PL.Models;

namespace PL.AutoMapperProfiles
{
    public class ReportViewProfile : Profile
    {
        public ReportViewProfile()
        {
            CreateMap<ReportDTO, ReportViewModel>();
        }
    }
}