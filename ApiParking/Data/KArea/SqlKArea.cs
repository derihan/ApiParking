using ApiParking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiParking.Data.KArea
{
    public class SqlKArea : IKAreaRepo
    {
        private kparkingContext _context;

        public SqlKArea(kparkingContext context)
        {
            _context = context;
        }

        public void CreateKArea(MdKategoriArea cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }
            _context.MdKategoriArea.Add(cmd);
        }

        public IEnumerable<MdKategoriArea> GetAllKArea()
        {
            return _context.MdKategoriArea.ToList();
        }

        public MdKategoriArea GetKAreaById(int id)
        {
            return _context.MdKategoriArea.FirstOrDefault(p => p.KatiAreaId == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public MdKategoriArea CheckData(string Kategori)
        {
            return _context.MdKategoriArea.FirstOrDefault(p => p.KatAreaName == Kategori);
        }

        public void UpdateKArea(Dictionary<String, string> data)
        {
            var store = _context.MdKategoriArea.Where(s => s.KatiAreaId == Convert.ToInt16(data["id"])).First();
            store.KatAreaName = data["name_kategori"];
            store.KatiAreaId = Convert.ToInt16(data["id"]);
        }

        public void DeleteKArea(MdKategoriArea mdKategori)
        {
            if (mdKategori == null)
            {
                throw new ArgumentNullException(nameof(mdKategori));
            }
            _context.MdKategoriArea.Remove(mdKategori);
        }

    }
}
