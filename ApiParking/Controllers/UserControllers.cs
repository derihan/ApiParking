﻿using ApiParking.Data.User;
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
using System.Text;
using Microsoft.Extensions.Configuration;
using ApiParking.Handler;
using Microsoft.AspNetCore.Authorization;
using BCrypt.Net;
using System.Text.RegularExpressions;
using System.Security.Claims;

namespace ApiParking.Controllers
{
    [Authorize]
    [Route("api/auth")]
    [ApiController]
    public class UserControllers : ControllerBase
    {
        private IConfiguration _config { get; }

        private readonly IUserRepository _repository;
        private readonly ICarsRepository _carrepo;
        private readonly ISlotRepo _slotrepo;
        private readonly IHistoryRepocs _history;
        private readonly kparkingContext kparking;
        private IJwtAuthenticationManager jwtAuth;
     
        private string message;
        private int states;
        private int dc;
        private string dataab;

        public UserControllers(
            IUserRepository repository, 
            ICarsRepository carrepo, 
            kparkingContext context, 
            ISlotRepo slotrepo, 
            IConfiguration config,
            IHistoryRepocs history,
            IJwtAuthenticationManager jwtauth
            
            )
        {
            _repository = repository;
            _carrepo = carrepo;
            kparking = context;
            _slotrepo = slotrepo;
            _history = history;
            _config = config;
            jwtAuth = jwtauth;
           
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public ActionResult login(MgUserParking user)
        {
            if (user != null)
            {
                var data = _repository.adminLogin(user.UserUsername, user.UserPassword);
                if (data != null)
                {

                   

                    var token = jwtAuth.Authenticate(data.UserUsername, data.UserPassword, data.UserId);
                    if (token == null)
                    {
                        return Unauthorized();
                    }
                    return Ok(token);
                }
                return NotFound();
            }
            else
            {
                return NotFound();
            }

        }

        [AllowAnonymous]
        [HttpGet("data-user-activity/{id}")]
        public ActionResult GetAllHistoryUser(int id)
        {
            var data = _repository.GetHitoryUser(id);
            if (data != null)
            {
                return StatusCode(200, new { data });
            }
            return NotFound();
        }

        [AllowAnonymous]
        [HttpGet("data-user-activity")]
        public ActionResult getUserACtivity(int id)
        {
            var data = _repository.GetUserActivity();
            if (data != null)
            {
                return StatusCode(200, new { data });
            }
            return NotFound();
        }

        [Authorize]
        [HttpGet]
        [Route("mobile-guest-data")]
        public ActionResult MobileGetData()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
           

            if (!String.IsNullOrEmpty(id.ToString()))
            {
                var datacm = _repository.MobileUserApi(id);
                return Ok(new {datacm });
            }
            return NotFound();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("guest-login")]
        public ActionResult GuestLoginByKode(ScabbModel scabb)
        {
          
            var histokode =  Regex.Replace(scabb.history_kode, "[^A-Za-z0-9]", " ");
            var datacm = _repository.getScanndata(histokode);

            if (datacm != null)
            {

                    List<UserModel> smk =new List<UserModel>((IEnumerable<UserModel>)datacm);
               
                    var token = jwtAuth.Authenticate(smk[0].user_username, smk[0].user_password, smk[0].user_id);
                    if (token == null)
                    {
                        return Unauthorized();
                    }
                    return Ok(token);
               
            }

            return NotFound();
        }

        [Authorize]
        [HttpPut("update-profil/{id}")]
        public ActionResult UpdateProfil(MgUserParking userParking)
        {
            var username = userParking.UserUsername;
            var userfullname = userParking.UserFullname;

            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            if (!String.IsNullOrEmpty(id.ToString()))
            {

                var dts = kparking.MgUserParking.Where(cv => cv.UserUsername == username).Count();

                if(dts < 1)
                {
                    var store = new Dictionary<String, string>();

                    store.Add("user_username", username);
                    store.Add("user_fullname", userfullname);
                    store.Add("id", id.ToString());

                    _repository.updateProfil(store, id);
                    var mock = _repository.SaveChanges();

                    if (mock)
                    {
                        message = "update succesfull";
                        states = 200;
                    }
                    else
                    {
                        message = "failed to update";
                        states = 404;
                    }

                    return Ok(new { msg = message, sts = states });
                }
                return Ok(new { msg = "Username sudah digunakan" } );
            }

            return NotFound();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("generate")]
        public  ActionResult generateQrcode(MgUserParking userParking)
        {
            Random rand = new Random();
            var number_p = userParking.PlateNumber;
            var password_b = userParking.UserPassword;
            var password_hash = BCrypt.Net.BCrypt.HashPassword(password_b, 12);
            var username = userParking.UserUsername;
            var comdDlot = _slotrepo.checkAvailable();

            if ( comdDlot != null)
            {
                if (String.IsNullOrEmpty(number_p))
                {
                    return StatusCode(200, new { data = "Sory license number cannot be null", state = 0 });
                }
                        
             
                        //add data user table
                        var store = new Dictionary<string, string>();
                        store.Add("plat", number_p);
                        store.Add("username", username);
                        store.Add("password", password_hash);
                        dc = _repository.UserRegistration(store);
                        Console.WriteLine("1");

                        _repository.createOtp(dc);
                        Console.WriteLine("2");

                        //add data to car table
                        var plates = new Dictionary<string, string>();
                        plates.Add("platenum", number_p);
                        plates.Add("userCars", Convert.ToString(dc));
                        _carrepo.CreateCar(plates);
                        Console.WriteLine("3");

                        //update data to slot table
                        var slots = new Dictionary<string, int>();
                        slots.Add("slotId", comdDlot.ParSlotId);
                        slots.Add("carUserId", dc);
                        _slotrepo.UpdateSlot(slots);
                        Console.WriteLine("4");

                        //insert to table history
                        var histo = new Dictionary<string, string>();
                        histo.Add("historyArea", Convert.ToString(comdDlot.ParAreaId));
                        histo.Add("license", number_p);
                        histo.Add("park_user_id",  Convert.ToString(dc));
                        dataab = _history.CreateHistory(histo);
                        Console.WriteLine("5");

                        states = 1;

                        return StatusCode(201, new { kode = dataab, data = comdDlot });

                  
               
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
