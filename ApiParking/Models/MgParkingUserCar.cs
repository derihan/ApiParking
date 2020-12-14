using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiParking.Models
{
    public partial class MgParkingUserCar
    {   
        [Key]
        public int ParkCarId { get; set; }
        [Required]
        public string ParkCarLicence { get; set; }

        public string ParkCarImage { get; set; }

        [Required]
        public string ParkCarUserId { get; set; }

        [Timestamp]
        public DateTime ParkCarCreatedAt { get; set; }
        public int ParkCarSts { get; set; } = 1;
    }
}
