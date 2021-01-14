using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiParking.Data.income
{
    public interface IncomeRepisitory
    {
        object getIncomeData();

        object AddIncomebyId(string kode);



    }
}
