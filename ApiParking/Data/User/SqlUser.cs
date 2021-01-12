using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiParking.Models;


namespace ApiParking.Data.User
{
    public class SqlUser : IUserRepository
    {
        private kparkingContext _context;
      
        public SqlUser(kparkingContext context)
        {
            _context = context;
        }

        public int UserRegistration(Dictionary<string, string> repos)
        {
            var _repos = new MgUserParking();
            _repos.UserPassword = repos["password"];
            _repos.UserUsername = repos["username"];
            _context.MgUserParking.Add(_repos);
            _context.SaveChanges();
            return _repos.UserId;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public MgUserParking adminLogin(string username, string password)
        {
            return _context.MgUserParking.Where(s => s.UserUsername == username && s.UserPassword == password).FirstOrDefault();
        }

        private static string GenerateNewRandom()
        {
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");
            if (r.Distinct().Count() == 1)
            {
                r = GenerateNewRandom();
            }
            return r;
        }

      

        public string createOtp(int userId)
        {

            var kode = Convert.ToInt32(GenerateNewRandom());
            var otp = new ParkingOtp();
            otp.OtpKode = kode;
            otp.OtpUserId = userId;

            _context.ParkingOtp.Add(otp);
            _context.SaveChanges();

            return Convert.ToString(otp);
        }

        public IEnumerable<MgUserParking> GetAllSlot()
        {
            throw new NotImplementedException();
        }
    }
}
