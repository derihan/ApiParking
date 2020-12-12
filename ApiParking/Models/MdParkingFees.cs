using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiParking.Models
{
    public partial class MdParkingFees
    {
        [Key]
        public int ParkFeesId { get; set; }

        [Required]
        public int ParkFeesValue { get; set; }

        [Timestamp]
        public DateTime CreatedAt { get; set; }

        [Required]
        public int ParkFeesSts { get; set; } = 1;
    }
}
