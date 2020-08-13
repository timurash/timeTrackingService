using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IReportService
    {
        ServiceResultDTO AddReport(ReportDTO reportDTO);
        ServiceResultDTO UpdateReport(ReportDTO reportDTO);
        ServiceResultDTO DeleteReport(int? reportId);
        GetReportsByDateDTO Get(ReportFilterDTO reportFilterDTO);
        void Dispose();
    }
}