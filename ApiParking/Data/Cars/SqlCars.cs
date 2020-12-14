using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiParking.Models;

namespace ApiParking.Data.Cars
{
    public class SqlCars : ICarsRepository
    {
        private kparkingContext _context;

        public SqlCars(kparkingContext context)
        {
            _context = context;
        }

       
        public void CreateCar(Dictionary<string, string> data)
        {
            var pslot = new MgParkingUserCar();
            pslot.ParkCarLicence = data["platenum"];
            pslot.ParkCarUserId = data["userCars"];
            _context.MgParkingUserCar.Add(pslot);
            _context.SaveChanges();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
