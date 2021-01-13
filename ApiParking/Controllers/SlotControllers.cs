using ApiParking.Data.Slot;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiParking.Models;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;

namespace ApiParking.Controllers
{
    [Authorize]
    [Route("api/data-slot")]
    [ApiController]
    public class SlotControllers : ControllerBase
    {
        private readonly ISlotRepo _repository;
        private string message;
        private int states;

        public SlotControllers(ISlotRepo slotRepo)
        {
            _repository = slotRepo;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<IEnumerable<SlotModels>> GetAllArea()
        {
            var commandItems = _repository.GetAllSlot();
            if (commandItems != null)
            {
                return Ok( new { data = commandItems });
            }
            return NotFound();
        }

        [AllowAnonymous]
        [HttpGet("{filter}", Name = "GetFiltersLOT")]
        public ActionResult<IEnumerable<SlotModels>> GetFilteRsLOT(string filter)
        {
            var fiktesan = Regex.Replace(filter, "[^A-Za-z0-9_.]", " ");

            var data =  _repository.GetFilter(fiktesan);
            if (data != null)
            {
                return StatusCode(200, new { data });
            }
            return NotFound();
        }

    }
}
