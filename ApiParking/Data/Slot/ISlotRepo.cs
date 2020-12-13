using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiParking.Models;

namespace ApiParking.Data.Slot
{
    public interface ISlotRepo
    {
        bool SaveChanges();
        IEnumerable<MgParkingSlot> GetAllSlot();

        void CreateSlot(Dictionary<string, int> data);
    }
}
