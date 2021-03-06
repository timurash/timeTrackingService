﻿namespace DAL.Entities
{
    /// <summary>
    /// Модель пользователя для работы с БД.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id пользователя.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Email пользователя.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Отчество пользователя.
        /// </summary>
        public string Patronymic { get; set; }
    }
}