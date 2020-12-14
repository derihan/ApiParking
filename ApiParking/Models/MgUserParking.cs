using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiParking.Models
{
    public partial class MgUserParking
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string UserUsername { get; set; }
        public string UserRole { get; set; } = "2";
        public string UserFullname { get; set; } = "Customers";
        [Required]
        public string UserPassword { get; set; }
        
        [Timestamp]
        public DateTime UserCraetedAt { get; set; }
        public int UsersSts { get; set; } = 2;
        [NotMapped]
        public string PlateNumber { get; set; }

    }
}
