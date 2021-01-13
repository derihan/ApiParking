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
        IEnumerable<SlotModels> GetAllSlot();

        //IEnumerable<MgParkingSlot> checkAvailable();
        MgParkingSlot checkAvailable();
        void CreateSlot(Dictionary<string, int> data);

        void UpdateSlot(Dictionary<string, int> data);
        IEnumerable<SlotModels> GetFilter(string filter);
    }
}
