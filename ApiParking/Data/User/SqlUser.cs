using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiParking.Data.History;
using ApiParking.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiParking.Data.User
{
    public class SqlUser : IUserRepository
    {
        private kparkingContext _context;
        private IHistoryRepocs _HistoryRepocs;
        private UserContext userContext;
      
        public SqlUser(kparkingContext context, IHistoryRepocs historyRepocs, UserContext xuser)
        {
            userContext = xuser;
            _HistoryRepocs = historyRepocs;
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

        public object GetHitoryUser(int id)
        {
            var data = _HistoryRepocs.GetHitoryUser(id);
            return data;
        }

        public object GetUserActivity()
        {
            return userContext.User.FromSqlRaw("SELECT ds.user_id,ds.user_username,ds.user_fullname" +
                ",ds.user_craeted_at,ds.users_sts,ds.user_role FROM mg_user_parking AS ds where ds.user_role=2").ToList();
        }
    }
}
