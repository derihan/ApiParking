using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiParking.Data.History;
using ApiParking.Models;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

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

        public object getScanndata(string histo)
        {
            var comd = userContext.User.FromSqlRaw("SELECT ds.user_password,ds.user_id,ds.user_username,ds.user_fullname" +
                ",ds.user_craeted_at,ds.users_sts,ds.user_role FROM mg_user_parking AS ds JOIN mg_park_history sh ON sh.park_user_id=ds.user_id where ds.user_role=2 and sh.hist_kode={0}",histo).ToList();
           
            return comd;
        }

        public Dictionary<string, string> MobileUserApi(int ids)
        {
            string connStr = "server=localhost;user=root;database=kparking;port=3306;password=";
            Dictionary<string, string> slm = new Dictionary<string, string>();
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string sql = String.Format("SELECT hi.hist_id,hi.hist_kode,ad.user_username,ad.user_fullname,es.park_fees_value,ar.area_number" +
                    ",de.kat_area_name,de.kat_number,hi.hist_in, hi.hist_out, hi.hist_sts, (SELECT cr.park_car_licence from mg_parking_user_car cr " +
                    "WHERE cr.park_car_user_id=ad.user_id and (cr.park_car_created_at BETWEEN CONCAT(CURRENT_DATE, ' 00:03:00') and CONCAT(CURRENT_DATE, ' 23:00:00')))as " +
                    "park_car_license FROM mg_park_history hi JOIN mg_user_parking ad on ad.user_id=hi.park_user_id " +
                    "JOIN mg_parking_area ar on ar.area_id=hi.hist_area_id join md_parking_fees es on es.park_fees_id=ar.area_parking_fees_id " +
                    "JOIN md_kategori_area de ON de.kati_area_id=ar.area_kategori_id WHERE ad.user_id={0}", ids );

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
               

                while (rdr.Read())
                {
                    slm.Add("user_id", rdr.GetInt32("area_number").ToString());
                    slm.Add("hist_kode", rdr.GetString("hist_kode"));
                    slm.Add("user_username", rdr.GetString("user_username"));
                    slm.Add("user_fullname", rdr.GetString("user_fullname"));
                    slm.Add("park_fees_value", rdr.GetInt32("park_fees_value").ToString());
                    slm.Add("area_number", rdr.GetInt16("area_number").ToString());
                    slm.Add("kat_number", rdr.GetInt16("kat_number").ToString());
                    slm.Add("hist_in", rdr.GetDateTime("hist_in").ToString());
                    slm.Add("hist_out", rdr["hist_out"] == DBNull.Value ? String.Empty : rdr.GetDateTime("hist_out").ToString());
                }

                return slm;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }
            conn.Close();
            return slm;
        }
    }
}
