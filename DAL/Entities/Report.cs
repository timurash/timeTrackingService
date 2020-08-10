using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    /// <summary>
    /// Модель отчета для работы с БД.
    /// </summary>
    public class Report
    {
        /// <summary>
        /// Id отчета.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Примечание к отчету.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Количество часов.
        /// </summary>
        public int Hours { get; set; }

        /// <summary>
        /// Дата создания отчета.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Id пользователя, создавшего отчет.
        /// </summary>
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
