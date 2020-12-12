using ApiParking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiParking.Data.Fees
{
    public class SqlFees : IFeesRepocs
    {
        private kparkingContext _context;

        public SqlFees(kparkingContext context)
        {
            _context = context;
        }

        public MdParkingFees CheckFees(int feesValues)
        {
            return _context.MdParkingFees.FirstOrDefault(p => p.ParkFeesValue == feesValues);
        }

        public void CreateFees(MdParkingFees mdParking)
        {
            if (mdParking == null)
            {
                throw new ArgumentNullException(nameof(mdParking));
            }
            _context.MdParkingFees.Add(mdParking);
        }

        public void DeleteFees(MdParkingFees mgParking)
        {
            if (mgParking == null)
            {
                throw new ArgumentNullException(nameof(mgParking));
            }
            _context.MdParkingFees.Remove(mgParking);
        }

        public IEnumerable<MdParkingFees> GetAllFees()
        {
            return _context.MdParkingFees.ToList();
        }

        public MdParkingFees GetFeesById(int id)
        {
            return _context.MdParkingFees.FirstOrDefault(p => p.ParkFeesId == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateFees(Dictionary<string, int> data)
        {
            var store = _context.MdParkingFees.Where(s => s.ParkFeesId == data["id"]).First();
            store.ParkFeesValue = data["feesValues"];
        }
    }
}
