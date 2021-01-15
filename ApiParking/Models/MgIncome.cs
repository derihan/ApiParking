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
    }
}
