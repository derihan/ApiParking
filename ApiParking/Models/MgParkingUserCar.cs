using System;
using System.Collections.Generic;

namespace ApiParking.Models
{
    public partial class MgParkingUserCar
    {
        public int ParkCarId { get; set; }
        public string ParkCarLicence { get; set; }
        public string ParkCarImage { get; set; }
        public string ParkCarUserId { get; set; }
        public DateTime ParkCarCreatedAt { get; set; }
        public int ParkCarSts { get; set; }
    }
}
