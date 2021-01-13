using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiParking.Data.History
{
    public class HistoryModel
    {
        [Key]
        public int hist_id { get; set; }
        public string hist_kode { get; set; }
        public string hist_area_id  { get; set; }
        public DateTime hist_in { get; set; }
        public DateTime? hist_out { get; set; }
        public int area_number { get; set; }
        public string kat_area_name { get; set; }
        public string user_fullname { get; set; }
        public int user_id { get; set; }
        public string user_username { get; set; }
        public int kat_number { get; set; }
        public int hist_sts { get; set; }
        public DateTime hist_created_atd { get; set; }
    }
}
