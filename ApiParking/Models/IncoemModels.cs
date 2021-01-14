using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiParking.Models
{
    public class IncoemModels
    {
        [Key]
        public int income_id { get; set; }
        public int hist_id { get; set; }
        public int income_value { get; set; }
        public DateTime? income_created_at { get; set; }
        public int income_sts { get; set; }
        public string hist_kode { get; set; }
        public int hist_area_id { get; set; }
        public DateTime hist_in { get; set; }
        public DateTime? hist_out { get; set; }
        public int  park_user_id { get; set; }
        public DateTime hist_created_at { get; set; }
        public int hist_sts { get; set; }
        public string user_fullname { get; set; }
        public string park_car_license { get; set; }
        public int kat_number { get; set; }
        public int area_number { get; set; }
        public string kat_area_name { get; set; }
    }
}
