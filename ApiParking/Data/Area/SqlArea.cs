using ApiParking;
using ApiParking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiParking.Data.Area
{
    public class SqlArea : IAreaRepo
    {
        private kparkingContext _context;

        public SqlArea(kparkingContext context)
        {
            _context = context;
        }

        public void CreateArea(MgParkingArea areas_m)
        {
            if (areas_m == null)
            {
                throw new ArgumentNullException(nameof(areas_m));
            }
            _context.MgParkingArea.Add(areas_m);
        }

        public IEnumerable<MgParkingArea> GetAllArea()
        {
            return _context.MgParkingArea.ToList();
        }

        public MgParkingArea GetAreaById(int id)
        {
            return _context.MgParkingArea.FirstOrDefault(p => p.AreaId == id);
        }

        
        public MgParkingArea CheckData(int number, int katid)
        {
            return _context.MgParkingArea.FirstOrDefault(p=> p.AreaNumber == number && p.AreaKategoriId == katid);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateArea(Dictionary<String, int> data)
        {
            var store = _context.MgParkingArea.Where(s=>s.AreaId == data["id"]).First();
            store.AreaNumber = data["areadNumber"];
            store.AreaKategoriId = data["areaKategori"];
            store.AreaParkingFeesId = data["areFees"];
           
        }

        public void DeleteArea(MgParkingArea mgParking)
        {
            if (mgParking == null)
            {
                throw new ArgumentNullException(nameof(mgParking));
            }
            _context.MgParkingArea.Remove(mgParking);
        }
    }
}

