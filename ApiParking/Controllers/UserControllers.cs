using ApiParking.Data.User;
using ApiParking.Data.Cars;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiParking.Models;
using ApiParking.Data;
using ApiParking.Data.Slot;
using ApiParking.Data.History;

namespace ApiParking.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class UserControllers : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly ICarsRepository _carrepo;
        private readonly ISlotRepo _slotrepo;
        private readonly IHistoryRepocs _history;
        private readonly kparkingContext kparking;
        private string message;
        private int states;
        public UserControllers(IUserRepository repository, ICarsRepository carrepo, kparkingContext context, ISlotRepo slotrepo, IHistoryRepocs history)
        {
            _repository = repository;
            _carrepo = carrepo;
            kparking = context;
            _slotrepo = slotrepo;
            _history = history;
        }

        //[HttpPost]
        //public async Task<ActionResult> regitrationUser(MgUserParking userParking)
        //{
        //    for admin only
        //}

        [HttpPost]
        public  ActionResult generateQrcode(MgUserParking userParking)
        {
            var number_p = userParking.PlateNumber;
            var password_b = userParking.UserPassword;
            var username = userParking.UserUsername;

            var comdDlot = _slotrepo.checkAvailable();

            if ( comdDlot != null)
            {
               

                if (String.IsNullOrEmpty(number_p))
                {
                    return StatusCode(200, new { data = "Sory license plat cannot be null", state = 0 });
                }
                var transaction = kparking.Database.BeginTransaction();
                try
                {
                    //add data user table
                    var store = new Dictionary<string, string>();
                    store.Add("plat", number_p);
                    store.Add("username", username);
                    store.Add("password", password_b);
                    var dc = _repository.UserRegistration(store);

                    //add data to car table
                    var plates = new Dictionary<string, string>();
                    plates.Add("platenum", number_p);
                    plates.Add("userCars", Convert.ToString(dc));
                    _carrepo.CreateCar(plates);

                    //update data to slot table
                    var slots = new Dictionary<string, int>();
                    slots.Add("slotId", comdDlot.ParSlotId);
                    slots.Add("carUserId", dc);
                    _slotrepo.UpdateSlot(slots);

                    //insert to table history
                    var histo = new Dictionary<string, int>();
                    histo.Add("historyArea", comdDlot.ParAreaId);
                    histo.Add("park_user_id", dc);
                    _history.CreateHistory(histo);
                   
                    transaction.Commit();
                    states = 1;
                    message = "save data success";
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    states = 0;
                    message = "Save data failed, check your connection";
                }
            }
            else
            {
                states = 0;
                message = "Sorry No available slot";
            }
            return StatusCode(200, new { data = message, state = states });
        }

    }
}
