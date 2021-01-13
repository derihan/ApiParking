using ApiParking.Data.Slot;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiParking.Models;
using Microsoft.AspNetCore.Authorization;

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

    }
}
