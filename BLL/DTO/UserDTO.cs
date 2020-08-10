using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    /// <summary>
    /// DTO для пользователей.
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// Id пользователя.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// E-mail пользователя.
        /// </summary>
        [Required (ErrorMessage = "Не указан Email.")]
        [EmailAddress (ErrorMessage = "Некорректный Email адрес.")]
        public string Email { get; set; }

        /// <summary>
        /// Имя пользователя. 
        /// </summary>
        [Required (ErrorMessage = "Не указано имя.")]
        public string Firstname { get; set; }

        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        [Required (ErrorMessage = "Не указана фамилия.")]
        public string Surname { get; set; }

        /// <summary>
        /// Отчество пользователя.
        /// </summary>
        public string Patronymic { get; set; }
    }
}