using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IReportService
    {
        void AddReport(ReportDTO reportDTO);
        void UpdateReport(ReportDTO reportDTO);
        void DeleteReport(int? reportId);
        IEnumerable<ReportDTO> Get(ReportFilterDTO reportFilterDTO);
        void Dispose();
    }
}
