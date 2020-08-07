using System;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository<User> Users { get; }
        IReportRepository<Report> Reports { get; }
        void Save();
    }
}