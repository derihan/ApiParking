using ApiParking.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiParking.Models
{
    public class MgParkingArea
    {
        [Key]
        public int AreaId { get; set; }

        [Required]
        public int AreaNumber { get; set; }

        [Required]
        public int AreaKategoriId { get; set; }

        [Required]
        public int AreaParkingFeesId { get; set; }

        [Timestamp]
        public DateTime AreaCreatedAt { get; set; }

        
        public int AreaSts { get; set; } = 1;



    }
}
