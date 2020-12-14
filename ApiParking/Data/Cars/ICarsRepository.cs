using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiParking.Models;


namespace ApiParking.Data.Cars
{
    public interface ICarsRepository
    {
        void CreateCar(Dictionary<string, string> data);

        bool SaveChanges();

       

    }
}
