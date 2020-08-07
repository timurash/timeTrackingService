namespace BLL.DTO
{
    // DTO для пользователей
    public class UserDTO
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Firstname { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }
    }
}
