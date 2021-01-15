using ApiParking.Models;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiParking.Data.income
{
    public class SqlIncome : IncomeRepisitory
    {
        private kparkingContext _context;

        public SqlIncome(kparkingContext context)
        {
            _context = context;
        }

        public object AddIncome()
        {
            throw new NotImplementedException();
        }

        public object AddIncomebyId(string kode)
        {

            string connStr = "server=localhost;user=root;database=kparking;port=3306;password=";

            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string sql = String.Format("SELECT hi.hist_id,hi.hist_kode,hi.hist_in,us.user_id,ds.par_slot_id,ds.park_slot_status," +
                    "ds.park_slot_sts,od.area_number,sc.kat_area_name,sc.kat_number,ds.park_slot_user_id, " +
                    "us.user_fullname,cv.park_fees_value, (SELECT DA.park_car_licence FROM mg_parking_user_car da WHERE da.park_car_user_id = us.user_id " +
                    " and da.park_car_created_at BETWEEN CONCAT(CURRENT_DATE, ' 00:00:00') and CONCAT(CURRENT_DATE, ' 23:00:00')  ORDER by DA.park_car_id DESC )AS park_car_license " +
                    "FROM mg_parking_slot ds JOIN mg_parking_area od ON od.area_id = ds.par_area_id JOIN mg_park_history hi ON hi.hist_area_id = od.area_id " +
                    "JOIN md_kategori_area sc ON sc.kati_area_id = od.area_kategori_id " +
                    "JOIN md_parking_fees cv ON cv.park_fees_id = od.area_parking_fees_id " +
                    "JOIN mg_user_parking us on us.user_id = ds.park_slot_user_id WHERE hi.hist_in BETWEEN CONCAT(CURRENT_DATE, ' 00:00:00') and CONCAT(CURRENT_DATE, ' 23:00:00') and " +
                    "hi.hist_kode = {0} ORDER by sc.kat_number,od.area_number",kode);

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                List<IncoemModels> slm = new List<IncoemModels>();
                
                while (rdr.Read())
                {

                    IncoemModels dso = new IncoemModels();
                    var pout = DateTime.Now;
                    Console.WriteLine(pout);
                    var pin = rdr.GetDateTime("hist_in");
                    TimeSpan hours = pout.Subtract(pin);
                    Console.WriteLine(hours.Hours);

                    dso.area_number = (int)(rdr.GetInt32("area_number"));
                    dso.kat_area_name = rdr.GetString("kat_area_name").ToString();
                    dso.kat_number = (int)(rdr.GetInt32("kat_number"));
                    dso.park_car_license = (rdr["park_car_license"] == DBNull.Value) ? String.Empty : rdr.GetString("park_car_license").ToString();
                    dso.hist_kode = rdr.GetString("hist_kode").ToString();
                    dso.hist_in = rdr.GetDateTime("hist_in");
                    dso.user_fullname = rdr.GetString("user_fullname").ToString();
                    dso.income_value = hours.Hours > 1 ?  (int)(rdr.GetInt32("park_fees_value")) * hours.Hours : (int)(rdr.GetInt32("park_fees_value"));

                    slm.Add(dso);
                    

                }

                return slm;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }
            conn.Close();

            return new List<SlotModels>();
        }

        public object getIncomeData()
        {
            throw new NotImplementedException();
        }

        private bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public bool SaveDataIncome(MgIncome income)
        {
            var history = _context.MgParkHistory.First(cv => cv.HistId.Equals(income.HistId) && cv.HistoryKode.Contains(income.HistKode));
          
            if (history != null)
            {
                var slotId = _context.MgParkingSlot.First(cv => cv.ParAreaId.Equals(history.HistAreaId));
                var userid = _context.MgUserParking.First(cv => cv.UserId.Equals(history.ParkUserId));

                history.HistOut = DateTime.Now;
                history.HistSts = 2;
                _context.SaveChanges();

                if (slotId != null)
                {
                    slotId.ParkSlotUserId = String.Empty;
                    slotId.ParkSlotSts = 1;
                    _context.SaveChanges();
                }

                if (userid != null)
                {
                    userid.UsersSts = 1;
                    _context.SaveChanges();
                }

                MgIncome pso = new MgIncome();
                pso.HistId = history.HistId;
                pso.IncomeValue = income.IncomeValue;
                _context.Add(pso);
                return SaveChanges();

            }

            return false;
        }
    }

 } 