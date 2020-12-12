using System;
using System.Collections.Generic;

namespace ApiParking.Models
{
    public partial class MgParkHistory
    {
        public int HistId { get; set; }
        public string HistAreaId { get; set; }
        public DateTime HistIn { get; set; }
        public DateTime HistOut { get; set; }
        public string ParkCarId { get; set; }
        public DateTime HistCreatedAtd { get; set; }
        public int HistSts { get; set; }
    }
}
