using ApiParking.Data.income;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiParking.Controllers
{
    [Route("api/income")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>

        private IncomeRepisitory _income;

        public ValuesController(IncomeRepisitory income)
        {
            _income = income;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{kode}")]
        public ActionResult Get(string kode)
        {
            if (kode != null)
            {
               var data = _income.AddIncomebyId(kode);
                return StatusCode(200, new { data = data });
            }
            return NotFound();
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
