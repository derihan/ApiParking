using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiParking.Models;

namespace ApiParking.Data.Fees
{
    public interface IFeesRepocs
    {
        bool SaveChanges();
        IEnumerable<MdParkingFees> GetAllFees();

        List<SelectedItemFee> GetToArray();
        MdParkingFees GetFeesById(int id);
        void CreateFees(MdParkingFees mdParking);
        MdParkingFees CheckFees(int feesValue);
        void UpdateFees(Dictionary<String, int> data);
        void DeleteFees(MdParkingFees mdParking);
       
    }
}
