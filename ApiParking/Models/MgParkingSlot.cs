using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiParking.Models
{
    public partial class MgParkingSlot
    {
        [Key]
        public int ParSlotId { get; set; }
        [Required]
        public int ParAreaId { get; set; }
        public string ParkSlotUserId { get; set; } = null;
        [Required]
        public string ParkSlotStatus { get; set; } = "available";
        [Timestamp]
        public DateTime ParkSlotCreated { get; set; }
        public int ParkSlotSts { get; set; } = 1;

        [NotMapped]
        public int AreaNumber { get; set; }
        [NotMapped]
        public string AreaKatName { get; set; }
        [NotMapped]
        public int FeesValue { get; set; }
        [NotMapped]
        public int KatNumber { get; set; }
    }
}
