﻿using ApiParking.Data.Fees;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using ApiParking.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ApiParking.Controllers
{
    [Authorize]
    [Route("api/data-fees")]
    [ApiController]
    public class FeesControllers : ControllerBase
    {
        private readonly IFeesRepocs _repository;
        private string message;
        private int states;

        public FeesControllers(IFeesRepocs repocs)
        {
            _repository = repocs;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MdParkingFees>> GetAllArea()
        {
            var commandItems = _repository.GetAllFees();
            if (commandItems != null)
            {
                return StatusCode(200, new { data = commandItems });
            }
            return NotFound();
        }

        [HttpGet("item")]
        public ActionResult<IEnumerable<MdParkingFees>> SelcetedItem()
        {
            var commandItems = _repository.GetToArray();

            if (commandItems == null)
            {
                return NotFound();
            }
            
            return StatusCode(200, new { data = commandItems });
        }

        [HttpGet("{id}", Name = "GetFeesById")]
        public ActionResult GetFeesById(int id)
        {
            var commandItems = _repository.GetFeesById(id);
            if (commandItems != null)
            {
                return Ok(commandItems);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult CreateFees(MdParkingFees parkingFees)
        {
            if (ModelState.IsValid)
            {
                var mark = _repository.CheckFees(parkingFees.ParkFeesValue);

                if (mark == null)
                {
                    _repository.CreateFees(parkingFees);
                    _repository.SaveChanges();

                    return CreatedAtAction(nameof(GetFeesById), new { Id = parkingFees.ParkFeesId }, parkingFees);
                }
                else
                {
                    return StatusCode(200, new { alert = "fees values exist on this setting, try other values" });
                }
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateArea(int id, MdParkingFees mdParking)
        {
            var _data = mdParking;
            if (ModelState.IsValid)
            {
                var comd = _repository.GetFeesById(id);
                if (comd == null)
                {
                    return NotFound();
                }
                else
                {
                    var cmdex = _repository.CheckFees(_data.ParkFeesValue);
                    if (cmdex == null)
                    {
                        var store = new Dictionary<string, int>();

                        store.Add("feesValues", _data.ParkFeesValue);
                        store.Add("id", id);

                        _repository.UpdateFees(store);
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
            var commandItems = _repository.GetFeesById(id);
            if (commandItems == null)
            {
                return NotFound();
            }
            else
            {
                _repository.DeleteFees(commandItems);
                var mpck = _repository.SaveChanges();

                if (mpck)
                {
                    return Ok(new { alert = true });
                }

                return NotFound();
            }

        }

    }
}
