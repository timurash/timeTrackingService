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

        // создание пользователя
        public void CreateUser(UserDTO userDTO)
        {
            if (userDTO.Email == "" || userDTO.Email == null)
                throw new ValidationException("Поле E-mail обязательно", "");

            User user = Database.Users.Find(userDTO.Email);

            if (user != null)
                throw new ValidationException("Пользователь с таким E-mail уже существует", "");

            if (userDTO.Firstname == "" || userDTO.Firstname == null)
                throw new ValidationException("Поле имени обязательно", "");

            if (userDTO.Surname == "" || userDTO.Surname == null)
                throw new ValidationException("Поле фамилии обязательно", "");

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

        // обновление данных о пользователе
        public void UpdateUser(UserDTO userDTO)
        {
            if (userDTO.Email == "" || userDTO.Email == null)
                throw new ValidationException("Поле E-mail обязательно", "");

            User user = Database.Users.Find(userDTO.Email);

            if (user != null && userDTO.Id != user.Id)
                throw new ValidationException("Другой пользователь с таким E-mail уже существует", "");

            if (userDTO.Firstname == "" || userDTO.Firstname == null)
                throw new ValidationException("Поле имени обязательно", "");

            if (userDTO.Surname == "" || userDTO.Surname == null)
                throw new ValidationException("Поле фамилии обязательно", "");

            user = Database.Users.Get(userDTO.Id);

            if (user == null)
                throw new ValidationException("Пользователь не найден", "");
            
            user.Email = userDTO.Email;
            user.Firstname = userDTO.Firstname;
            user.Surname = userDTO.Surname;
            user.Patronymic = userDTO.Patronymic;
            
            Database.Users.Update(user);
            Database.Save();
        }

        // удаление пользователя
        public void DeleteUser(int? userId)
        {
            if (userId == null)
                throw new ValidationException("Id пользователя не установлено", "");

            User user = Database.Users.Get(userId.Value);

            if (user == null)
                throw new ValidationException("Пользователь не найден", "");

            // удаление всех отчетов, принадлежащих этому пользователю
            IEnumerable<Report> reportsToDelete = Database.Reports.GetByUserId(userId.Value);
            foreach (Report report in reportsToDelete)
            {
                Database.Reports.Delete(report.Id);
            }

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