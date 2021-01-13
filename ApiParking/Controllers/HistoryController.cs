using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using ApiParking.Data.History;
using System.Text.RegularExpressions;

namespace ApiParking.Controllers
{
    
    [Route("api/data-history")]
    [ApiController]
    public class HistoryController : ControllerBase
    {

        IHistoryRepocs _repository;
        public HistoryController(IHistoryRepocs repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<Object> GetAllArea()
        {
            var data = _repository.GetAllArea();
            if (data != null)
            {
                return StatusCode(200, new { data });
            }
            return NotFound();
        }

        [HttpGet("{filter}", Name = "GetFiltered")]
        public ActionResult<Object> GetFiltered(string filter)
        {
            var fiktesan = Regex.Replace(filter, "[^A-Za-z0-9_.]", " ");
            var data = _repository.GetFilter(fiktesan);
            if (data != null)
            {
                return StatusCode(200, new { data });
            }
            return NotFound();
        }


    }
}
