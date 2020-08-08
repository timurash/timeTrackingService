using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    // DTO для отчетов
    public class ReportDTO
    {
        public int? Id { get; set; }

        [Required (ErrorMessage = "Не указан Id пользователя")]
        public int? UserId { get; set; }

        [Required (ErrorMessage = "Не указано примечание")]
        public string Note { get; set; }

        [Required (ErrorMessage = "Не указано количество часов")]
        public int? Hours { get; set; }

        [Required (ErrorMessage = "Не указана дата")]
        public DateTime? Date { get; set; }
    }
}
