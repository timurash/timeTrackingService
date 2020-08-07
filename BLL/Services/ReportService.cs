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

        //  добавление отчета
        public void AddReport(ReportDTO reportDTO)
        {
            if (reportDTO.UserId == null)
                throw new ValidationException("Поле Id пользователя обязательно", "");

            User user = Database.Users.Get(reportDTO.UserId.Value);

            if (user == null)
                throw new ValidationException("Пользователь не найден", "");

            if (reportDTO.Date == null)
                throw new ValidationException("Поле даты обязательно", "");

            if (reportDTO.Hours == null)
                throw new ValidationException("Поле часов обязательно", "");

            if (reportDTO.Note == null)
                throw new ValidationException("Поле примечания обязательно", "");

            Report report = new Report
            {
                UserId = reportDTO.UserId.Value,
                Note = reportDTO.Note,
                Hours = reportDTO.Hours.Value,
                Date = reportDTO.Date.Value
            };

            Database.Reports.Add(report);
            Database.Save();
        }

        // обновление отчета
        public void UpdateReport(ReportDTO reportDTO)
        {
            if (reportDTO.UserId == null)
                throw new ValidationException("Поле Id пользователя обязательно", "");

            User user = Database.Users.Get(reportDTO.UserId.Value);

            if (user == null)
                throw new ValidationException("Пользователь не найден", "");

            if (reportDTO.Date == null)
                throw new ValidationException("Поле даты обязательно", "");

            if (reportDTO.Hours == null)
                throw new ValidationException("Поле часов обязательно", "");

            if (reportDTO.Note == null || reportDTO.Note == "")
                throw new ValidationException("Поле примечания обязательно", "");

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

        // удаление отчета
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

        // метод получения списка отчетов пользователя за указанный месяц
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
