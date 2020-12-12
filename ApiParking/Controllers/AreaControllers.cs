using ApiParking.Data.Area;
using ApiParking.Models;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiParking.Controllers
{
    [Route("api/data-area")]
    [ApiController]
    public class AreaControllers : ControllerBase
    {

        private readonly IAreaRepo _repository;
        private string message;
        private int states;

        public AreaControllers(IAreaRepo repository)
        {
            _repository = repository;

        }

        [HttpGet]
        public ActionResult<IEnumerable<MgParkingArea>> GetAllArea()
        {

            var commandItems = _repository.GetAllArea();
            if (commandItems != null)
            {
                return Ok(commandItems);
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
            if (ModelState.IsValid)
            {
                var mark = _repository.CheckData(mgParkingArea.AreaNumber, mgParkingArea.AreaKategoriId);

                if (mark == null)
                {
                    _repository.CreateArea(mgParkingArea);
                    _repository.SaveChanges();

                    return CreatedAtAction(nameof(GetAreaById), new { Id = mgParkingArea.AreaId }, mgParkingArea);
                }
                else
                {
                    return StatusCode(200, new { alert = "number exist on this room, try other number" });
                }
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
