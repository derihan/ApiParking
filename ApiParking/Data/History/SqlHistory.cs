using ApiParking.Models;
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

        public SqlHistory(kparkingContext context)
        {
            _context = context;
        }



        public string CreateHistory(Dictionary<string, string> data)
        {

            var pslot = new MgParkHistory();
            var datahist = _context.MgParkHistory.ToList();
            var comparedate = DateTime.Now.ToShortDateString();

            var counter = 0;

            foreach (var item in datahist)
            {
                if (item.HistCreatedAtd.ToShortDateString() == comparedate )
                {
                    counter += 1;
                }
            }

            var dataFormating = String.Format("000{0}{1}", DateTime.Now.ToString("ddMMyyyy"), (counter + 1) );
            pslot.HistAreaId = Convert.ToString(data["historyArea"]);
            pslot.ParkUserId = Convert.ToString(data["park_user_id"]);
            pslot.HistoryKode = dataFormating;
            _context.MgParkHistory.Add(pslot);
            _context.SaveChanges();
            return dataFormating;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
