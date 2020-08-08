using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;
using System.Linq;

namespace DAL.Repositories
{
    public class ReportRepository : IReportRepository<Report>
    {
        private UsersContext db;

        public ReportRepository(UsersContext context)
        {
            this.db = context;
        }

        public void Add(Report report)
        {
            db.Reports.Add(report);
        }

        public void Update(Report report)
        {
            db.Entry(report).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Report report = db.Reports.Find(id);
            if (report != null)
                db.Reports.Remove(report);
        }

        public void DeleteByUser(int userId)
        {
            db.Reports.RemoveRange(db.Reports.Where(x => x.UserId == userId));
        }

        public Report GetById(int id)
        {
            return db.Reports.Find(id);
        }

        public IEnumerable<Report> GetByUserAndDate(int userId, int month, int year)
        {
            return db.Reports.Where(p => p.UserId == userId && p.Date.Month == month && p.Date.Year == year);
        }

        public IEnumerable<Report> GetByUserId(int userId)
        {
            return db.Reports.Where(p => p.UserId == userId);
        }
    }
}