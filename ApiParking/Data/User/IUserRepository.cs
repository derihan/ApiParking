using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiParking.Data.History;
using ApiParking.Models;

namespace ApiParking.Data.User
{
    public interface IUserRepository
    {
        bool SaveChanges();
        int UserRegistration(Dictionary<string, string> data);

        object getScanndata(string histo);

        string createOtp(int userId);

        void updateProfil(Dictionary<String, String> stored, int id);

        object GetHitoryUser(int id);

        object GetUserActivity();

        Dictionary<String, String> MobileUserApi(int ids);

        MgUserParking adminLogin(string username, string password);
    }
}
