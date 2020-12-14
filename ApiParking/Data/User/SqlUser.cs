using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
