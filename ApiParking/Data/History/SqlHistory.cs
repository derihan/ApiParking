using ApiParking.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ApiParking.Data.History
{
    public class SqlHistory : IHistoryRepocs
    {
        private kparkingContext _context;
        private HistoryContext _hostcontext;

        public SqlHistory(kparkingContext context, HistoryContext historyContext)
        {
            _context = context;
            _hostcontext = historyContext;

        }



        public string CreateHistory(Dictionary<string, string> data)
        {

            var pslot = new MgParkHistory();
            var datahist = _context.MgParkHistory.ToList();
            var comparedate = DateTime.Now.ToShortDateString();

            var counter = 0;

            foreach (var item in datahist)
            {
                if (item.HistCreatedAtd.ToShortDateString() == comparedate)
                {
                    counter += 1;
                }
            }

            var dataFormating = String.Format("000{0}{1}", DateTime.Now.ToString("ddMMyyyy"), (counter + 1));
            pslot.HistAreaId = Convert.ToString(data["historyArea"]);
            pslot.ParkUserId = Convert.ToString(data["park_user_id"]);
            pslot.HistoryKode = dataFormating;
            _context.MgParkHistory.Add(pslot);
            _context.SaveChanges();
            return dataFormating;
        }

        public object GetFilter(string filter)
        {
            Object data = new { };

            data = _hostcontext.History.FromSqlRaw("SELECT ds.user_id,h.hist_kode,h.hist_sts,h.hist_created_atd, h.hist_area_id, h.hist_in, h.hist_out, ha.area_number, " +
               "ak.kat_area_name, ds.user_fullname, ds.user_username,h.hist_id from mg_park_history h JOIN mg_parking_area ha on ha.area_id = h.hist_area_id " +
               "JOIN md_kategori_area ak ON ak.kati_area_id = ha.area_kategori_id JOIN mg_user_parking ds ON ds.user_id = h.park_user_id")
                .Where(xc => Convert.ToString(xc.area_number) == filter || xc.kat_area_name.Contains(filter) || xc.hist_kode.Contains(filter) || xc.user_username.Contains(filter) ).ToList();

            return data;

        }

        public Object GetAllArea()
        {
            Object data = new { };

            data = _hostcontext.History.FromSqlRaw("SELECT ds.user_id,h.hist_kode,h.hist_sts,h.hist_created_atd, h.hist_area_id, h.hist_in, h.hist_out, ha.area_number, " +
                "ak.kat_area_name, ds.user_fullname, ds.user_username,h.hist_id from mg_park_history h JOIN mg_parking_area ha on ha.area_id = h.hist_area_id " +
                "JOIN md_kategori_area ak ON ak.kati_area_id = ha.area_kategori_id JOIN mg_user_parking ds ON ds.user_id = h.park_user_id").ToList();


            return data;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public object GetHitoryUser(int id)
        {
            Object data = new { };

            data = _hostcontext.History.FromSqlRaw("SELECT ds.user_id,h.hist_kode,h.hist_sts,h.hist_created_atd, h.hist_area_id, h.hist_in, h.hist_out, ha.area_number, " +
               "ak.kat_area_name, ds.user_fullname, ds.user_username,h.hist_id from mg_park_history h JOIN mg_parking_area ha on ha.area_id = h.hist_area_id " +
               "JOIN md_kategori_area ak ON ak.kati_area_id = ha.area_kategori_id JOIN mg_user_parking ds ON ds.user_id = h.park_user_id")
                .Where(xc => xc.user_id == id ).ToList();
            return data;

        }
    }
}
