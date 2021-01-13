using ApiParking.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiParking.Data.Slot
{
    public class SlotContext : DbContext
    {
        public SlotContext(DbContextOptions<SlotContext> options)
          : base(options)
        {
        }
        public DbSet<SlotModels> Slotcon { get; set; }
    }
}
