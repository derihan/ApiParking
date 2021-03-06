﻿
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiParking.Data.KArea;
using ApiParking.Models;
using Microsoft.AspNetCore.Authorization;

namespace ApiParking.Controllers
{
    [Authorize]
    [Route("api/data-kategori")]
    [ApiController]
    
    public class KAreaControllers : ControllerBase
    {
        private readonly IKAreaRepo _repository;
        private string message;
        private int states;
        private int state;

        public KAreaControllers(IKAreaRepo repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MdKategoriArea>> GetAllArea()
        {
            var commandItems = _repository.GetAllKArea();
            if (commandItems.Count() > 0)
            {
                return StatusCode(200, new { alert = "Data found", data = commandItems });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id}", Name = "GetKAreaById")]
        public ActionResult GetKAreaById(int id)
        {
            var commandItems = _repository.GetKAreaById(id);
            if (commandItems != null)
            {
                return StatusCode(200, new { alert = "Data found", data = commandItems });
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult CreateKategori(MdKategoriArea mdKategoriArea)
        {
            if (ModelState.IsValid)
            {
                var mark = _repository.CheckData(mdKategoriArea);

                if (mark == null)
                {
                    _repository.CreateKArea(mdKategoriArea);
                    _repository.SaveChanges();

                    return StatusCode(200, new { alert = "Simpan data sukses" });
                }
                else
                {
                    return StatusCode(200, new { alert = "Kategori exist on this Table, try other Kategori" });
                }
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateArea(int id, MdKategoriArea mdKategori)
        {
            var _data = mdKategori;
            if (ModelState.IsValid)
            {
                var comd = _repository.GetKAreaById(id);
                if (comd == null)
                {
                    return NotFound();
                }
                else
                {
                    var cmdex = _repository.CheckData(mdKategori);
                    if (cmdex == null)
                    {
                        var store = new Dictionary<String, string>();

                        store.Add("name_kategori", _data.KatAreaName);
                        store.Add("kat_number", _data.KatNumber.ToString());
                        store.Add("id", Convert.ToString(id));

                        _repository.UpdateKArea(store);
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

                        return StatusCode(states, new { alert = message,state = state });
                    }
                    return StatusCode(200, new { alert = "Data exist" , state = 2});
                }
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteKArea(int id)
        {
            var commandItems = _repository.GetKAreaById(id);
            if (commandItems == null)
            {
                return NotFound();
            }
            else
            {
                var data =  _repository.DeleteKArea(commandItems);

                if (data)
                {
                    var mpck = _repository.SaveChanges();

                    if (mpck)
                    {
                        message = "delete data succesfull";
                        states = 200;
                        state = 1;
                    }
                    else
                    {
                        message = "delete data failed";
                        states = 404;
                        state = 0;
                    }
                }
                else
                {
                    message = "restricted data, used by another table ";
                    states = 200;
                    state = 0;
                }

                return StatusCode(states, new { alert = message, state = state });
            }

        }
    }
}
