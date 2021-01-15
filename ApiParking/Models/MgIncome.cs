using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiParking.Models
{
    public partial class MgIncome
    {
        [Key]
        public int IncomeId { get; set; }
        public int HistId { get; set; }
        public double IncomeValue { get; set; }
        public DateTime IncomeCreatedAt { get; set; }
        public int IncomeSts { get; set; }

        [NotMapped]
        public string HistKode { get; set; }

        [NotMapped]
        public string HistAreaId { get; set; }

        [NotMapped]
        public string user_fullname { get; set; }

        [NotMapped]
        [Column("hist_in")]
        public DateTime hist_in { get; set; }
        [NotMapped]
        [Column("hist_out")]
        public DateTime hist_out { get; set; }
    }
}
