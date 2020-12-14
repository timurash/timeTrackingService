using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    /// <summary>
    /// DTO для метода получения отчетов по определнному пользователю за конкретный месяц.
    /// </summary>
    public class ReportFilterDTO
    {
        /// <summary>
        /// Id пользователя, отчеты которого необходимо предоставить.
        /// </summary>
        //[Required (ErrorMessage = "Id пользователя является обязательным полем.")]
        public int? UserId { get; set; }

        /// <summary>
        /// Месяц, за который необходимо предоставить отчеты.
        /// </summary>
        //[Required(ErrorMessage = "Месяц является обязательным поле.")]
        public int? Month { get; set; }

        /// <summary>
        /// Год, за который необходимо предоставить отчеты.
        /// </summary>
        //[Required(ErrorMessage = "Год является обязательным полем.")]
        public int? Year { get; set; }

        /// <summary>
        /// Метод для получения строки с данными, которые были переданы в DTO.
        /// Используется при записи логов в случае возникновения ошибок.
        /// </summary>
        public string GetValueString()
        {
            return ($" Данные DTO: UserId = {UserId}; Month = {Month}; Year = {Year}.").ToString();
        }
    }
}