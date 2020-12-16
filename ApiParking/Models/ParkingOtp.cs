using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiParking.Models
{
    public partial class ParkingOtp
    {
        [Key]
        public int OtpId { get; set; }
        [Required]
        public int OtpKode { get; set; }
        public int OtpUserId { get; set; } 
        [Timestamp]
        public DateTime Created { get; set; }
        public int OtpSts { get; set; } = 1;
      
    }
}
