using System;

namespace BLL.DTO
{
    // DTO для отчетов
    public class ReportDTO
    {
        public int? Id { get; set; }

        public int? UserId { get; set; }

        public string Note { get; set; }

        public int? Hours { get; set; }

        public DateTime? Date { get; set; }
    }
}
