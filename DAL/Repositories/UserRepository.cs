using System.Collections.Generic;
using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;
using System.Linq;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository<User>
    {
        private UsersContext db;

        public UserRepository(UsersContext context)
        {
            this.db = context;
        }

        /// <summary>
        /// Получение полного списка пользователей
        /// </summary>
        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        /// <summary>
        /// Поиск пользователя по Id
        /// </summary>
        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public void Create(User user)
        {
            db.Users.Add(user);
        }

        public void Update(User user)
        {
            db.Users.Update(user);
        }

        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
            }
        }

        /// <summary>
        /// Поиск пользователя по Email
        /// </summary>
        public User Find(string email)
        {
            return db.Users.Where(p => p.Email == email).FirstOrDefault();
        }
    }
}
