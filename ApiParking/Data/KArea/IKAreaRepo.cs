using ApiParking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiParking.Data.KArea
{
    public interface IKAreaRepo
    {
        bool SaveChanges();
        IEnumerable<MdKategoriArea> GetAllKArea();
        MdKategoriArea GetKAreaById(int id);
        void CreateKArea(MdKategoriArea Areas_kategori);
        MdKategoriArea CheckData(MdKategoriArea md);
        void UpdateKArea(Dictionary<String, string> data );
        bool DeleteKArea(MdKategoriArea mdKategori);

    }
}
