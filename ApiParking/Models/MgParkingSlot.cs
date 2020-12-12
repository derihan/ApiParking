using System;
using System.Collections.Generic;

namespace ApiParking.Models
{
    public partial class MgParkingSlot
    {
        public int ParSlotId { get; set; }
        public string ParAreaId { get; set; }
        public string ParkSlotUserId { get; set; }
        public string ParkSlotStatus { get; set; }
        public DateTime ParkSlotCreated { get; set; }
        public int ParkSlotSts { get; set; }
    }
}
