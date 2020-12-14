using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiParking.Models;

namespace ApiParking.Data.History
{
    public interface IHistoryRepocs
    {
        void CreateHistory(Dictionary<string, int> data);

        bool SaveChanges();
    }
}
