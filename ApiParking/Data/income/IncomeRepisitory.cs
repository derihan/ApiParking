using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiParking.Models;

namespace ApiParking.Data.income
{
    public interface IncomeRepisitory
    {
     
        IncoemModels AddIncomebyId(string kode);

        bool SaveDataIncome(MgIncome income);
        IEnumerable<IncoemModels> getAll();
    }
}
