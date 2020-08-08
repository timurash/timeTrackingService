using System;
using DAL.Interfaces;
using DAL.EF;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private UsersContext db;
        private UserRepository userRepository;
        private ReportRepository reportRepository;

        public EFUnitOfWork(UsersContext context)
        {
            db = context;
        }

        public IUserRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public IReportRepository<Report> Reports
        {
            get
            {
                if (reportRepository == null)
                    reportRepository = new ReportRepository(db);
                return reportRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}