﻿using ApiParking.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [NotMapped]
        public string kategori { get; set; }

        [NotMapped]
        public int FessVal { get; set; }

    

        
    }
}
