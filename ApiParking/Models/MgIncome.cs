using System;
using System.Collections.Generic;

namespace ApiParking.Models
{
    public partial class MgIncome
    {
        public int IncomeId { get; set; }
        public int HistId { get; set; }
        public double IncomeValue { get; set; }
        public int IncomeCreatedAt { get; set; }
        public int IncomeSts { get; set; }
    }
}
