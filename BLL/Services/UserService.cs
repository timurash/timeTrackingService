using BLL.Interfaces;
using BLL.DTO;
using DAL.Entities;
using DAL.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using BLL.AutomapperProfiles;

namespace BLL.Services
{
    /// <summary>
    /// Сервис для работы с пользователями.
    /// </summary>
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        /// <summary>
        /// Создание пользователя.
        /// </summary>
        public ServiceResultDTO CreateUser(UserDTO userDTO)
        {
            User user = Database.Users.Find(userDTO.Email);

            if (user != null)
                return new ServiceResultDTO("Пользователь с таким E-mail уже существует.");

            user = new User
            {
                Email = userDTO.Email,
                Firstname = userDTO.Firstname,
                Surname = userDTO.Surname,
                Patronymic = userDTO.Patronymic
            };

            Database.Users.Create(user);
            Database.Save();

            return new ServiceResultDTO(true);
        }

        /// <summary>
        /// Обновление данных о пользователе.
        /// </summary>
        public ServiceResultDTO UpdateUser(UserDTO userDTO)
        {
            User user = Database.Users.Get(userDTO.Id);

            if (user == null)
                return new ServiceResultDTO("Пользователь не найден.");

            User user2 = Database.Users.Find(userDTO.Email);

            if (user2 != null && userDTO.Id != user2.Id)
                return new ServiceResultDTO("Другой пользователь с таким E-mail уже существует.");

            user.Email = userDTO.Email;
            user.Firstname = userDTO.Firstname;
            user.Surname = userDTO.Surname;
            user.Patronymic = userDTO.Patronymic;
            
            Database.Users.Update(user);
            Database.Save();

            return new ServiceResultDTO(true);
        }

        /// <summary>
        /// Удаление пользователя.
        /// </summary>
        public ServiceResultDTO DeleteUser(int? userId)
        {
            User user = Database.Users.Get(userId.Value);

            if (user == null)
                return new ServiceResultDTO("Пользователь не найден.");

            // удаление всех отчетов, принадлежащих этому пользователю
            Database.Reports.DeleteByUser(userId.Value);

            Database.Users.Delete(userId.Value);
            Database.Save();

            return new ServiceResultDTO(true);
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, List<UserDTO>>(Database.Users.GetAll());
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}