using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;
using System.Linq;

namespace DAL.Repositories
{
    /// <summary>
    /// Репозиторий для работы с отчетами. 
    /// </summary>
    public class ReportRepository : IReportRepository<Report>
    {
        private readonly UsersContext db;

        public ReportRepository(UsersContext context)
        {
            this.db = context;
        }

        /// <summary>
        /// Добавление нового отчета.
        /// </summary>
        /// <param name="report"></param>
        public void Add(Report report)
        {
            db.Reports.Add(report);
        }

        /// <summary>
        /// Обновление существующего отчета.
        /// </summary>
        /// <param name="report"></param>
        public void Update(Report report)
        {
            db.Entry(report).State = EntityState.Modified;
        }

        /// <summary>
        /// Удаление отчета по определнному Id.
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            Report report = db.Reports.Find(id);
            if (report != null)
                db.Reports.Remove(report);
        }

        /// <summary>
        /// Удаление всех отчетов, созданных определенным пользователем.
        /// </summary>
        public void DeleteByUser(int userId)
        {
            db.Reports.RemoveRange(db.Reports.Where(x => x.UserId == userId));
        }

        /// <summary>
        /// Получение отчета по Id.
        /// </summary>
        public Report GetById(int id)
        {
            return db.Reports.Find(id);
        }

        /// <summary>
        /// Получение отчетов за указанный месяц, созданных определенным пользователем.
        /// </summary>
        public IEnumerable<Report> GetByUserAndDate(int userId, int month, int year)
        {
            return db.Reports.Where(p => p.UserId == userId && p.Date.Month == month && p.Date.Year == year);
        }

        /// <summary>
        /// Получение отчетов, относящихся к определенному пользователю.
        /// </summary>
        public IEnumerable<Report> GetByUserId(int? userId)
        {
            return db.Reports.Where(p => p.UserId == userId);
        }

        public IEnumerable<Report> GetAllReports()
        {
            return db.Reports.OrderBy(report => report.Id);
        }
    }
}