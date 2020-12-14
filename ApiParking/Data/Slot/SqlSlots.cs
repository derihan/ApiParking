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

        public MgParkingSlot checkAvailable()
        {
            return _context.MgParkingSlot.Where(a => a.ParkSlotSts == 1 && a.ParkSlotUserId == "" ).FirstOrDefault();
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

        public void UpdateSlot(Dictionary<string, int> data)
        {

            var store = _context.MgParkingSlot.Where(s => s.ParSlotId == data["slotId"]).First();
            store.ParkSlotUserId = Convert.ToString(data["carUserId"]);
            store.ParkSlotSts = 2;
            _context.SaveChanges();


        }
    }
}
