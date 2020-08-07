namespace BLL.DTO
{
    // DTO для метода получения отчетов по определнному пользователю за конкретный месяц
    public class ReportFilterDTO
    {
        public int? UserId { get; set; }

        public int? Month { get; set; }

        public int? Year { get; set; }
    }
}