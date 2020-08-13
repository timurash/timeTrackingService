using System.Collections.Generic;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUserRepository<T> where T : class
    {
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        IEnumerable<T> GetAll();
        User Get(int id);
        T Find(string email);
    }
}
