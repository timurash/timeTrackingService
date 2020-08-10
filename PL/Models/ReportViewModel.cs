using System;

namespace PL.Models
{
    /// <summary>
    /// Модель представления отчета.
    /// </summary>
    public class ReportViewModel
    {
        /// <summary>
        /// Id отчета.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id пользователя, создавшего отчет.
        /// </summary>
        public int UserId { get; set; }

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
    }
}