using System.Collections.Generic;

namespace BLL.DTO
{
    /// <summary>
    /// DTO для метода получения отчетов у определенного пользователя за конкретный месяц.
    /// </summary>
    public class GetReportsByDateDTO
    {
        /// <summary>
        /// Конструктор, используемый в случае возникновения ошибки валидации. 
        /// </summary>
        public GetReportsByDateDTO(string message)
        {
            ServiceResultDTO = new ServiceResultDTO(message);
        }

        /// <summary>
        /// Конкструктор, используемый в случае успешного выполнения метода.
        /// </summary>
        public GetReportsByDateDTO(IEnumerable<ReportDTO> reportDTOs)
        {
            Reports = reportDTOs;
            ServiceResultDTO = new ServiceResultDTO(true);
        }

        /// <summary>
        /// Коллекция для хранения полученных отчетов.
        /// </summary>
        public IEnumerable<ReportDTO> Reports { get; set; }

        /// <summary>
        /// DTO для передачи состояния валидации.
        /// </summary>
        public ServiceResultDTO ServiceResultDTO { get; set; }
    }
}