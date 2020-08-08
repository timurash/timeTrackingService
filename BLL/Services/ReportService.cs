using BLL.Interfaces;
using DAL.Interfaces;
using BLL.DTO;
using DAL.Entities;
using System.Collections.Generic;
using AutoMapper;
using BLL.Infrastructure;

namespace BLL.Services
{
    public class ReportService : IReportService
    {
        IUnitOfWork Database { get; set; }

        public ReportService(IUnitOfWork uow)
        {
            Database = uow;
        }

        /// <summary>
        /// добавление отчета
        /// </summary>
        public void AddReport(ReportDTO reportDTO)
        {
            User user = Database.Users.Get(reportDTO.UserId.Value);

            if (user == null)
                throw new ValidationException("Пользователь не найден", "");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ReportDTO, Report>()).CreateMapper();
            Report report = mapper.Map<ReportDTO, Report>(reportDTO);

            Database.Reports.Add(report);
            Database.Save();
        }

        /// <summary>
        /// обновление отчета
        /// </summary>
        public void UpdateReport(ReportDTO reportDTO)
        {

            User user = Database.Users.Get(reportDTO.UserId.Value);

            if (user == null)
                throw new ValidationException("Пользователь не найден", "");

            Report report = Database.Reports.GetById(reportDTO.Id.Value);

            if (report == null)
                throw new ValidationException("Отчет с таким Id не найден", "");

            report.UserId = reportDTO.UserId.Value;
            report.Note = reportDTO.Note;
            report.Hours = reportDTO.Hours.Value;
            report.Date = reportDTO.Date.Value;

            Database.Reports.Update(report);
            Database.Save();
        }

        /// <summary>
        /// удаление отчета
        /// </summary>
        public void DeleteReport(int? reportId)
        {
            if (reportId == null)
                throw new ValidationException("Id отчета не установлено", "");

            Report report = Database.Reports.GetById(reportId.Value);

            if (report == null)
                throw new ValidationException("Отчет с таким Id не найден", "");

            Database.Reports.Delete(reportId.Value);
            Database.Save();
        }
        /// <summary>
        /// метод получения списка отчетов пользователя за указанный месяц
        /// </summary>
        public IEnumerable<ReportDTO> Get(ReportFilterDTO reportFilterDTO)
        {
            if (reportFilterDTO.UserId == null)
                throw new ValidationException("Не указан Id пользователя", "");

            if (reportFilterDTO.Month == null)
                throw new ValidationException("Не указан месяц, за который необходимо предоставить отчеты", "");

            if (reportFilterDTO.Year == null)
                throw new ValidationException("Не указан год, за который необходимо предоставить отчеты", "");

            User user = Database.Users.Get(reportFilterDTO.UserId.Value);

            if (user == null)
                throw new ValidationException("Пользователь не найден", "");

            var reports = Database.Reports.GetByUserAndDate(reportFilterDTO.UserId.Value, reportFilterDTO.Month.Value, reportFilterDTO.Year.Value);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Report, ReportDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Report>, List<ReportDTO>>(reports);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
