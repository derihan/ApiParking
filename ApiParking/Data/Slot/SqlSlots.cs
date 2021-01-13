using ApiParking.Models;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ApiParking.Data.Slot
{
    public class SqlSlots : ISlotRepo
    {
        private kparkingContext _context;
        private SlotContext Slotsc;
        public SqlSlots(kparkingContext context, SlotContext slotContext)
        {
            Slotsc = slotContext;
            _context = context;
        }

        public MgParkingSlot checkAvailable()
        {
            var data = _context.MgParkingSlot.Where(a => a.ParkSlotSts == 1 && a.ParkSlotUserId == "").Join(
                _context.MgParkingArea,
                pslot => pslot.ParAreaId,
                parea => parea.AreaId,
                (pslot, parea) => new
                {
                    SlotId = pslot.ParSlotId,
                    AreaId = parea.AreaId,
                    AreaNumber = parea.AreaNumber,
                    AreaKotId = parea.AreaKategoriId,
                    AreafEEs = parea.AreaParkingFeesId
                }).Join(
                _context.MdKategoriArea,
                areap => areap.AreaKotId,
                katp => katp.KatiAreaId,
                (areap, katp) => new
                {
                    SlotId = areap.SlotId,
                    AreaId = areap.AreaId,
                    AreaNumber = areap.AreaNumber,
                    AreaKotId = areap.AreaKotId,
                    AreaKatName = katp.KatAreaName,
                    AreaFeesId = areap.AreafEEs,
                    KatNumber = katp.KatNumber
                }).Join(
                _context.MdParkingFees,
                area => area.AreaFeesId,
                feed => feed.ParkFeesId,
                (area, feed) => new
                {
                    AreaNumber = area.AreaNumber,
                    AreaKatName = area.AreaKatName,
                    AreaFess = feed.ParkFeesValue,
                    ParSlotId = area.SlotId,
                    AreaId = area.AreaId,
                    KatNUmber = area.KatNumber
                }).OrderBy(xc => xc.KatNUmber).ThenBy(uc => uc.AreaNumber).FirstOrDefault();
            if (data == null)
            {
                return new MgParkingSlot();
            }
            else
            {
                return new MgParkingSlot
                {
                    AreaNumber = data.AreaNumber,
                    AreaKatName = data.AreaKatName,
                    KatNumber = data.KatNUmber,
                    FeesValue = data.AreaFess,
                    ParSlotId = data.ParSlotId,
                    ParAreaId = data.AreaId
                };
            }
        }

        public void CreateSlot(Dictionary<string, int> _data)
        {
            var pslot = new MgParkingSlot
            {
                ParAreaId = _data["ParAreaId"]
            };

            _context.MgParkingSlot.Add(pslot);
            SaveChanges();
        }


        private ObservableCollection<SlotModels> myVar;

        public ObservableCollection<SlotModels> Slotfilter
        {
            get { return myVar; }
            set { myVar = value; }
        }



        public IEnumerable<SlotModels> GetAllSlot()
        {

            string connStr = "server=localhost;user=root;database=kparking;port=3306;password=";

            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string sql = "SELECT us.user_id,ds.par_slot_id,ds.park_slot_status,ds.park_slot_sts,od.area_number,sc.kat_area_name,sc.kat_number, " +
                    "ds.park_slot_user_id, us.user_username, " +
                    "(SELECT DA.park_car_licence FROM mg_parking_user_car da " +
                    "WHERE da.park_car_user_id=us.user_id and da.park_car_created_at " +
                    "BETWEEN CONCAT(CURRENT_DATE,' 05:00:00') and CONCAT(CURRENT_DATE, ' 23:00:00') " +
                    "ORDER by DA.park_car_id DESC )AS park_car_license FROM mg_parking_slot ds " +
                    "JOIN mg_parking_area od ON od.area_id=ds.par_area_id JOIN md_kategori_area sc " +
                    "ON sc.kati_area_id=od.area_kategori_id JOIN md_parking_fees cv " +
                    "ON cv.park_fees_id=od.area_parking_fees_id " +
                    "LEFT JOIN mg_user_parking us on us.user_id=ds.park_slot_user_id ORDER by sc.kat_number,od.area_number";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                List<SlotModels> slm = new List<SlotModels>();

                while (rdr.Read())
                {

                    SlotModels dso = new SlotModels();

                    dso.area_number = (int)(rdr.GetInt32("area_number"));
                    dso.kat_area_name = rdr.GetString("kat_area_name").ToString();
                    dso.kat_number = (int)(rdr.GetInt32("kat_number"));
                    dso.park_car_license = (rdr["park_car_license"] == DBNull.Value) ? String.Empty : rdr.GetString("park_car_license").ToString();
                    dso.park_slot_status = rdr.GetString("park_slot_status").ToString();
                    dso.park_slot_sts = (int)rdr.GetInt32("park_slot_sts");
                    dso.park_slot_user_id = (rdr["park_slot_user_id"] == DBNull.Value) ? String.Empty : rdr.GetString("park_slot_user_id").ToString();
                    dso.par_slot_id = (int)rdr.GetInt32("par_slot_id");
                    dso.user_username = (rdr["user_username"] == DBNull.Value) ? String.Empty : rdr.GetString("user_username").ToString();

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

        public IEnumerable<SlotModels> GetFilter(string filter)
        {


            ObservableCollection<SlotModels> sda = new ObservableCollection<SlotModels>(GetAllSlot());
            Slotfilter = sda;

            return Slotfilter.Where(cx => Convert.ToString(cx.kat_area_name).Contains(filter) || cx.area_number.ToString().Contains(filter) || cx.user_username.Contains(filter) );

        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateSlot(Dictionary<string, int> data)
        {

            var store = _context.MgParkingSlot.Where(s => s.ParSlotId == data["slotId"]).First();
            store.ParkSlotUserId = Convert.ToString(data["carUserId"]);
            store.ParkSlotSts = 2;
            _context.SaveChanges();


        }



    }
}
