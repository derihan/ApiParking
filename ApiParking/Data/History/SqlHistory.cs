using ApiParking.Models;
using System;
using System.Collections.Generic;
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

        public void CreateHistory(Dictionary<string, int> data)
        {
            var pslot = new MgParkHistory();

            pslot.HistAreaId = Convert.ToString(data["historyArea"]);
            pslot.ParkUserId = Convert.ToString(data["park_user_id"]);
            _context.MgParkHistory.Add(pslot);
            _context.SaveChanges();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
