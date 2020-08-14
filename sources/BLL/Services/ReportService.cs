using BLL.Interfaces;
using DAL.Interfaces;
using BLL.DTO;
using DAL.Entities;
using System.Collections.Generic;
using AutoMapper;

namespace BLL.Services
{
    /// <summary>
    /// Сервис для работы с отчетами.
    /// </summary>
    public class ReportService : IReportService
    {
        IUnitOfWork Database { get; set; }

        private readonly IMapper _mapper;

        public ReportService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }

        /// <summary>
        /// Добавление отчета.
        /// </summary>
        public ServiceResultDTO AddReport(ReportDTO reportDTO)
        {
            User user = Database.Users.Get(reportDTO.UserId.Value);

            if (user == null)
                return new ServiceResultDTO("Пользователь не найден.");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ReportDTO, Report>()).CreateMapper();
            Report report = mapper.Map<ReportDTO, Report>(reportDTO);

            Database.Reports.Add(report);
            Database.Save();

            return new ServiceResultDTO(true);
        }

        /// <summary>
        /// Обновление отчета.
        /// </summary>
        public ServiceResultDTO UpdateReport(ReportDTO reportDTO)
        {
            User user = Database.Users.Get(reportDTO.UserId.Value);

            if (user == null)
                return new ServiceResultDTO("Пользователь не найден.");

            Report report = Database.Reports.GetById(reportDTO.Id.Value);

            if (report == null)
                return new ServiceResultDTO("Отчет с таким Id не найден.");

            report.UserId = reportDTO.UserId.Value;
            report.Note = reportDTO.Note;
            report.Hours = reportDTO.Hours.Value;
            report.Date = reportDTO.Date.Value;

            Database.Reports.Update(report);
            Database.Save();

            return new ServiceResultDTO(true);
        }

        /// <summary>
        /// Удаление отчета.
        /// </summary>
        public ServiceResultDTO DeleteReport(int? reportId)
        {
            if (reportId == null)
                return new ServiceResultDTO("Id отчета не установлено.");

            Report report = Database.Reports.GetById(reportId.Value);

            if (report == null)
                return new ServiceResultDTO("Отчет с таким Id не найден.");

            Database.Reports.Delete(reportId.Value);
            Database.Save();

            return new ServiceResultDTO(true);
         ;
        }
        /// <summary>
        /// Метод получения списка отчетов пользователя за указанный месяц.
        /// </summary>
        public GetReportsByDateDTO Get(ReportFilterDTO reportFilterDTO)
        {
            if (reportFilterDTO.UserId == null)
                return new GetReportsByDateDTO("Не указан Id пользователя.");

            if (reportFilterDTO.Month == null)
                return new GetReportsByDateDTO("Не указан месяц, за который необходимо предоставить отчеты.");

            if (reportFilterDTO.Year == null)
                return new GetReportsByDateDTO("Не указан год, за который необходимо предоставить отчеты.");

            User user = Database.Users.Get(reportFilterDTO.UserId.Value);

            if (user == null)
                return new GetReportsByDateDTO("Пользователь не найден.");

            var reports = Database.Reports.GetByUserAndDate(reportFilterDTO.UserId.Value, reportFilterDTO.Month.Value, reportFilterDTO.Year.Value);

            GetReportsByDateDTO result = new GetReportsByDateDTO(_mapper.Map<IEnumerable<ReportDTO>>(reports));
            return result;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}