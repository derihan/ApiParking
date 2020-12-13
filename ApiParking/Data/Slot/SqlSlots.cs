using ApiParking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiParking.Data.Slot
{
    public class SqlSlots : ISlotRepo
    {
        private kparkingContext _context;
        public SqlSlots(kparkingContext context)
        {
            _context = context;
        }

        public void CreateSlot(Dictionary<string, int> _data)
        {
            var pslot = new MgParkingSlot 
            {
                ParAreaId = _data["ParAreaId"]
            };

            _context.MgParkingSlot.Add(pslot);
            SaveChanges();
        }

        public IEnumerable<MgParkingSlot> GetAllSlot()
        {
            return _context.MgParkingSlot.ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
