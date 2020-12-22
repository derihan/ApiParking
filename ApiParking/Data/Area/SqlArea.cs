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

        public List<MgParkingArea> GetAllArea()
        {
            List<MgParkingArea> genericArea = new List<MgParkingArea>();
            MgParkingArea areaObject;
            var areadata = _context.MgParkingArea
                    .Join(_context.MdKategoriArea,
                        tbarea => tbarea.AreaKategoriId, 
                        tbkat => tbkat.KatiAreaId,
                        (tbarea, tbkat) => new
                        {
                            AreaId = tbarea.AreaId,
                            AreaNumber = tbarea.AreaNumber,
                            Kategori = tbkat.KatAreaName,
                            FeesId = tbarea.AreaParkingFeesId,
                            AreaCreatedAt = tbarea.AreaCreatedAt,
                        })
                    .Join(_context.MdParkingFees, 
                        a => a.FeesId,
                        b => b.ParkFeesId,
                        (a, b) => new
                        {
                            AreaId = a.AreaId,
                            AreaNumber = a.AreaNumber,
                            FeesValue = b.ParkFeesValue,
                            Kategori = a.Kategori,
                            AreaCreatedAt = a.AreaCreatedAt
                        }).ToList();

            foreach (var item in areadata)
            {
                areaObject = new MgParkingArea
                {
                    AreaId = item.AreaId,
                    AreaNumber = item.AreaNumber,
                    AreaCreatedAt = item.AreaCreatedAt,
                    kategori = item.Kategori,
                    FessVal = item.FeesValue
                };
                genericArea.Add(areaObject);
            }
            return genericArea;
        }

        public MgParkingArea GetAreaById(int id)
        {
            var areadata = _context.MgParkingArea
                   .Join(_context.MdKategoriArea,
                       tbarea => tbarea.AreaKategoriId,
                       tbkat => tbkat.KatiAreaId,
                       (tbarea, tbkat) => new
                       {
                           AreaId = tbarea.AreaId,
                           AreaNumber = tbarea.AreaNumber,
                           Kategori = tbkat.KatAreaName,
                           FeesId = tbarea.AreaParkingFeesId,
                           AreaCreatedAt = tbarea.AreaCreatedAt,
                       })
                   .Join(_context.MdParkingFees,
                       a => a.FeesId,
                       b => b.ParkFeesId,
                       (a, b) => new
                       {
                           AreaId = a.AreaId,
                           AreaNumber = a.AreaNumber,
                           FeesValue = b.ParkFeesValue,
                           Kategori = a.Kategori,
                           AreaCreatedAt = a.AreaCreatedAt
                       }).Where(p => p.AreaId == id).FirstOrDefault();

            return new MgParkingArea
            {
                AreaId = areadata.AreaId,
                AreaNumber = areadata.AreaNumber,
                kategori = areadata.Kategori,
                FessVal = areadata.FeesValue
            };
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

        public bool DeleteArea(int id)
        {
            var data = _context.MgParkingArea.First(a => a.AreaId == id);
            if (data != null)
            {
                _context.MgParkingArea.Remove(data);
                return SaveChanges();
            }
            return false;
        }
    }
}

