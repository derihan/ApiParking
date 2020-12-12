using System;
using System.Collections.Generic;

namespace ApiParking.Models
{
    public partial class MgUserParking
    {
        public int UserId { get; set; }
        public string UserUsername { get; set; }
        public string UserRole { get; set; }
        public string UserFullname { get; set; }
        public string UserPassword { get; set; }
        public DateTime UserCraetedAt { get; set; }
        public int UsersSts { get; set; }
    }
}
