using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiParking.Models
{

    public class SlotModels
    {
        [Key]
        public int par_slot_id { get; set; }
        public string park_slot_status { get; set; }
        public int park_slot_sts { get; set; }
        public int area_number { get; set; }
        public int kat_number { get; set; }
        public string kat_area_name { get; set; }
        public string park_slot_user_id { get; set; }
        public string user_username { get; set; }

       [NotMapped]
        public string park_car_license { get; set; }
    }
}
