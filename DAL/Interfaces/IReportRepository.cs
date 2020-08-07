using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IReportRepository<T> where T : class
    {
        void Add(T item);
        void Update(T item);
        void Delete(int id);
        T GetById(int id);
        IEnumerable<T> GetByUserAndDate(int userId, int month, int year);
        IEnumerable<T> GetByUserId(int userId);
    }
}