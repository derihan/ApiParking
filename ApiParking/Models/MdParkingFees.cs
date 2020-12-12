using System;
using System.Collections.Generic;

namespace ApiParking.Models
{
    public partial class MdParkingFees
    {
        public int ParkFeesId { get; set; }
        public double ParkFeesValue { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ParkFeesSts { get; set; }
    }
}
