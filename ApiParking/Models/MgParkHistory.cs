using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiParking.Models
{
    public partial class MgParkHistory
    {
        [Key]
        public int HistId { get; set; }
        public string HistAreaId { get; set; }

        public string HistoryKode { get; set; }

        [Timestamp]
        public DateTime HistIn { get; set; }
        public DateTime? HistOut { get; set; } = null;
        public string ParkUserId { get; set; }

        [Timestamp]
        public DateTime HistCreatedAtd { get; set; }
        public int HistSts { get; set; } = 1;
    }
}
