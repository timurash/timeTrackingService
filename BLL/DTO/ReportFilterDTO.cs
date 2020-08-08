using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    // DTO для метода получения отчетов по определнному пользователю за конкретный месяц
    public class ReportFilterDTO
    {
        [Required (ErrorMessage = "Id пользователя является обязательным полем")]
        public int? UserId { get; set; }

        [Required(ErrorMessage = "Месяц является обязательным поле")]
        public int? Month { get; set; }

        [Required(ErrorMessage = "Год является обязательным полем")]
        public int? Year { get; set; }
    }
}