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
            var data = _context.MgParkingSlot.Where(a => a.ParkSlotSts == 1 && a.ParkSlotUserId == "").Join(
                _context.MgParkingArea,
                pslot => pslot.ParAreaId,
                parea => parea.AreaId,
                (pslot, parea) => new
                {
                    SlotId = pslot.ParSlotId,
                    AreaId = parea.AreaId,
                    AreaNumber = parea.AreaNumber,
                    AreaKotId = parea.AreaKategoriId,
                    AreafEEs = parea.AreaParkingFeesId
                }).Join(
                _context.MdKategoriArea,
                areap => areap.AreaKotId,
                katp => katp.KatiAreaId,
                (areap, katp) => new
                {
                    SlotId = areap.SlotId,
                    AreaId = areap.AreaId,
                    AreaNumber = areap.AreaNumber,
                    AreaKotId = areap.AreaKotId,
                    AreaKatName = katp.KatAreaName,
                    AreaFeesId = areap.AreafEEs
                }).Join(
                _context.MdParkingFees,
                area => area.AreaFeesId,
                feed => feed.ParkFeesId,
                (area, feed) => new
                {
                    AreaNumber = area.AreaNumber,
                    AreaKatName = area.AreaKatName,
                    AreaFess = feed.ParkFeesValue,
                    ParSlotId = area.SlotId,
                    AreaId = area.AreaId
                }).FirstOrDefault();
            if (data == null)
            {
                return new MgParkingSlot();
            }
            else
            {
                return new MgParkingSlot
                {
                    AreaNumber = data.AreaNumber,
                    AreaKatName = data.AreaKatName,
                    FeesValue = data.AreaFess,
                    ParSlotId = data.ParSlotId,
                    ParAreaId = data.AreaId
                };
            }
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
