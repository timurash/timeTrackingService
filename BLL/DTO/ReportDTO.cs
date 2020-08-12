using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    /// <summary>
    /// DTO для отчетов.
    /// </summary>
    public class ReportDTO
    {
        /// <summary>
        /// Id отчета.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Id пользователя, создавшего отчет.
        /// </summary>
        [Required (ErrorMessage = "Не указан Id пользователя.")]
        public int? UserId { get; set; }

        /// <summary>
        /// Примечание к отчету.
        /// </summary>
        [Required (ErrorMessage = "Не указано примечание.")]
        public string Note { get; set; }

        /// <summary>
        /// Количество часов.
        /// </summary>
        [Required (ErrorMessage = "Не указано количество часов.")]
        public int? Hours { get; set; }

        /// <summary>
        /// Дата создания отчета.
        /// </summary>
        [Required (ErrorMessage = "Не указана дата.")]
        [DataType(DataType.DateTime)]
        public DateTime? Date { get; set; }

        /// <summary>
        /// Метод для получения строки с данными, которые были переданы в DTO.
        /// Используется при записи логов в случае возникновения ошибок.
        /// </summary>
        public string GetValueString()
        {
            return ($" Данные DTO: Id = {Id}; UserId = {UserId}; Note = {Note}; Hours = {Hours}; Date = {Date}.").ToString();
        }
    }
}