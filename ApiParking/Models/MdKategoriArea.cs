using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiParking.Models
{
    public partial class MdKategoriArea
    {
        [Key]
        public int KatiAreaId { get; set; }

        [Required]
        public string KatAreaName { get; set; }

        [Timestamp]
        public DateTime CreatedAt { get; set; }
       
        public int KatAreaSts { get; set; } = 1;
    }
}
