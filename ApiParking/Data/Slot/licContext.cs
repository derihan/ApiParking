using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiParking.Data.Slot
{
    public class licContext : DbContext
    {
        public licContext(DbContextOptions<licContext> options)
         : base(options)
        {
        }



    }
}
