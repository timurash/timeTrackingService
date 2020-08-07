using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Report
    {
        public int Id { get; set; }

        public string Note { get; set; }

        public int Hours { get; set; }

        public DateTime Date { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
