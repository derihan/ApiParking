using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiParking.Models
{
    public class UserModel 
    {
        [Key]
        public int user_id { get; set; }
        public string user_username { get; set; }
        public string user_password { get; set; }

        public string user_role { get; set; }

        public DateTime user_craeted_at { get; set; }
        public int users_sts { get; set; }
    }
}
