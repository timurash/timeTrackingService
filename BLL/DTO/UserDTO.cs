using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    // DTO для пользователей
    public class UserDTO
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Не указан Email")]
        [EmailAddress (ErrorMessage = "Некорректный Email адрес")]
        public string Email { get; set; }

        [Required (ErrorMessage = "Не указано имя")]
        public string Firstname { get; set; }

        [Required (ErrorMessage = "Не указана фамилия")]
        public string Surname { get; set; }

        public string Patronymic { get; set; }
    }
}
