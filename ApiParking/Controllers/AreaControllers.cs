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
        private kparkingContext _context;
        private IConfiguration _config;

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
                return Ok(commandItems);
            }
            return NotFound();
        }

        [HttpPost]

        public ActionResult CreateArea(MgParkingArea mgParkingArea)
        {
            
            var mark = _repository.CheckData(mgParkingArea.AreaNumber, mgParkingArea.AreaKategoriId);
            if (mark == null)
            {
                var trasnsaction = _context.Database.BeginTransaction();
                try
                {
                    _repository.CreateArea(mgParkingArea);
                    _repository.SaveChanges();
                    var store = new Dictionary<string, int>();
                    store.Add("ParAreaId", mgParkingArea.AreaId);
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
                    return StatusCode(201, new { alert = "Add data successful" });
                }
                else
                {
                    return StatusCode(400, new { alert = "Connection problem save data failed" });
                }

            }

            return StatusCode(200, new { alert = "number exist on this room, try other number" });
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
                    var cmdex = _repository.CheckData(_data.AreaNumber, _data.AreaKategoriId);
                    if (cmdex == null)
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
                        }
                        else
                        {
                            message = "failed to update";
                            states = 404;
                        }

                        return StatusCode(states, new { alert = message });
                    }
                    return StatusCode(200, new { alert = "Data exist" });
                }
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteArea(int id)
        {
            var commandItems = _repository.GetAreaById(id);
            if (commandItems == null)
            {
                return NotFound();
            }
            else
            {
                _repository.DeleteArea(commandItems);
                var mpck = _repository.SaveChanges();

                if (mpck)
                {
                    message = "delete data succesfull";
                    states = 200;
                }
                else
                {
                    message = "delete data failed";
                    states = 404;
                }
                return StatusCode(states, new { alert = message });
            }
           
        }
            
    }
}
