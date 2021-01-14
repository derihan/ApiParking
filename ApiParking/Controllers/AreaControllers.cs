using ApiParking.Data.Area;
using ApiParking.Data.Slot;
using ApiParking.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using ApiParking.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace ApiParking.Controllers
{
    [Authorize]
    [Route("api/data-area")]
    [ApiController]
    public class AreaControllers : ControllerBase
    {
        
        private readonly IAreaRepo _repository;
        private readonly ISlotRepo _slotrepo;
        private string message;
        private int states;
        private int state;
        private kparkingContext _context;
        private IConfiguration _config;
        private MgParkingArea getItem;

        public AreaControllers(
            IAreaRepo repository,
            ISlotRepo slotrepo,
            kparkingContext context,
            IConfiguration config
            )
        {
            _context = context;
            _repository = repository;
            _slotrepo = slotrepo;
        }


        [HttpGet]
        public ActionResult<List<MgParkingArea>> GetAllArea()
        {
            var commandItems = _repository.GetAllArea();
            if (commandItems != null)
            {
                return StatusCode(200, new { data = commandItems });
            }
            return NotFound();
        }

        [HttpGet("{id}", Name = "GetAreaById")]
        public ActionResult GetAreaById(int id)
        {
            var commandItems = _repository.GetAreaById(id);
            if (commandItems != null)
            {
                return StatusCode(200, commandItems);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult CreateArea(MgParkingArea mgParkingArea)
        {
            
            var mark = _repository.CheckData(mgParkingArea);
            if (mark < 1 )
            {
               
                var trasnsaction = _context.Database.BeginTransaction();
                try
                {
                    _repository.CreateArea(mgParkingArea);
                    _repository.SaveChanges();
                    var store = new Dictionary<string, int>();
                    store.Add("ParAreaId", mgParkingArea.AreaId);
                    getItem = _repository.GetAreaById(mgParkingArea.AreaId);
                    _slotrepo.CreateSlot(store);
                    trasnsaction.Commit();
                    states = 1;

                }
                catch (Exception)
                {
                    trasnsaction.Rollback();
                    states = 0;                
                }

                if (states == 1)
                {
                    return StatusCode(201, new { alert = "Add data successful", data = getItem });
                }
                else
                {
                    return NotFound();
                }

            }

            return NotFound();
        }

        [HttpGet("sort/{filter}")]
       
        public ActionResult FilterArea(string filter)
        {
            var fiktesan = Regex.Replace(filter, "[^A-Za-z0-9_.]", " ");
            var commandItems = _repository.GetFilter(fiktesan);
            if (commandItems != null)
            {
                return StatusCode(200, new {  data = commandItems } );
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateArea(int id, MgParkingArea mgParkingArea)
        {
            var _data = mgParkingArea;
            if (ModelState.IsValid)
            {
                var comd = _repository.GetAreaById(id);
                if (comd == null)
                {
                    return NotFound();
                }
                else
                {
                    var cmdex = _repository.CheckData(_data);
                    Console.WriteLine(cmdex);
                    if (cmdex < 1)
                    {
                        var store = new Dictionary<String, int>();

                        store.Add("areadNumber", _data.AreaNumber);
                        store.Add("areaKategori", _data.AreaKategoriId);
                        store.Add("areFees", _data.AreaParkingFeesId);
                        store.Add("id", id);

                        _repository.UpdateArea(store);
                        var mock = _repository.SaveChanges();
                      
                        if (mock)
                        {
                            message = "update succesfull";
                            states = 200;
                            state = 1;
                        }
                        else
                        {
                            message = "failed to update";
                            states = 200;
                            state = 0;

                        }

                        return StatusCode(states, new { alert = message, state = state });
                    }
                    return StatusCode(200, new { alert = "Data exist", state = 0 });
                }
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteArea(int id)
        {
           
            if (String.IsNullOrEmpty(Convert.ToString(id)))
            {
                return StatusCode(200, new { alert = "failed" });
            }
            else
            {
                var mpck = _repository.DeleteArea(id);
                if (mpck)
                {
                    return Ok(new { alert = "sukses" });
                }
                return StatusCode(200, new { alert = "failed" });
            }

        }

    }
}
