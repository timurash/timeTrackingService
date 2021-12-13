using System.Collections.Generic;
using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;
using System.Linq;

namespace DAL.Repositories
{
    /// <summary>
    /// Репозиторий для работы с пользователями.
    /// </summary>
    public class UserRepository : IUserRepository<User>
    {
        private UsersContext db;

        public UserRepository(UsersContext context)
        {
            this.db = context;
        }

        /// <summary>
        /// Получение полного списка пользователей.
        /// </summary>
        public IEnumerable<User> GetAll()
        {
            return db.Users.OrderBy(user => user.Id);
        }

        /// <summary>
        /// Поиск пользователя по Id.
        /// </summary>
        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        /// <summary>
        /// Создание нового пользователя.
        /// </summary>
        /// <param name="user"></param>
        public void Create(User user)
        {
            db.Users.Add(user);
        }

        /// <summary>
        /// Обновление данных о существующем пользователе.
        /// </summary>
        public void Update(User user)
        {
            db.Users.Update(user);
        }

        /// <summary>
        /// Удаление пользователя по Id.
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
            }
        }

        /// <summary>
        /// Поиск пользователя по Email.
        /// </summary>
        public User Find(string email)
        {
            return db.Users.Where(p => p.Email == email).FirstOrDefault();
        }

        /// <summary>
        /// Проверка уникальности E-mail при создании пользователя
        /// </summary>
        public bool CheckForUniqueEmail(string email)
        {
            var result = db.Users.Where(p => p.Email == email).ToList();
            return result.Count() == 0;
        }
    }
}
