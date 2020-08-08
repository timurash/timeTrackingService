using BLL.Interfaces;
using BLL.DTO;
using DAL.Entities;
using DAL.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using BLL.Infrastructure;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        /// <summary>
        /// создание пользователя
        /// </summary>
        public void CreateUser(UserDTO userDTO)
        {
            User user = Database.Users.Find(userDTO.Email);

            if (user != null)
                throw new ValidationException("Пользователь с таким E-mail уже существует", "");

            user = new User
            {
                Email = userDTO.Email,
                Firstname = userDTO.Firstname,
                Surname = userDTO.Surname,
                Patronymic = userDTO.Patronymic
            };

            Database.Users.Create(user);
            Database.Save();
        }

        /// <summary>
        /// обновление данных о пользователе
        /// </summary>
        public void UpdateUser(UserDTO userDTO)
        {
            User user = Database.Users.Get(userDTO.Id);

            if (user == null)
                throw new ValidationException("Пользователь не найден", "");

            User user2 = Database.Users.Find(userDTO.Email);

            if (user2 != null && userDTO.Id != user2.Id)
                throw new ValidationException("Другой пользователь с таким E-mail уже существует", "");

            user.Email = userDTO.Email;
            user.Firstname = userDTO.Firstname;
            user.Surname = userDTO.Surname;
            user.Patronymic = userDTO.Patronymic;
            
            Database.Users.Update(user);
            Database.Save();
        }

        /// <summary>
        /// удаление пользователя
        /// </summary>
        public void DeleteUser(int? userId)
        {
            User user = Database.Users.Get(userId.Value);

            if (user == null)
                throw new ValidationException("Пользователь не найден", "");

            // удаление всех отчетов, принадлежащих этому пользователю
            Database.Reports.DeleteByUser(userId.Value);

            Database.Users.Delete(userId.Value);
            Database.Save();
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, List<UserDTO>>(Database.Users.GetAll());
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}