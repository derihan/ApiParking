using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiParking.Models;

namespace ApiParking.Data.History
{
    public interface IHistoryRepocs
    {
        string CreateHistory(Dictionary<string, string> data);

        bool SaveChanges();
       
    }
}
