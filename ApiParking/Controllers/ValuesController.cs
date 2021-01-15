using ApiParking.Data.income;
using ApiParking.Models;
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
        public ActionResult<IEnumerable<MgIncome>> Get()
        {
            var comand = _income.getAll();
            if (comand != null)
            {
                return Ok(new { data = comand });
            }
            return NotFound();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{kode}")]
        public ActionResult Get(string kode)
        {
            if (kode != null)
            {
               var data = _income.AddIncomebyId(kode);
                return StatusCode(200,data);
            }
            return NotFound();
        }

        // POST api/<ValuesController>
        [HttpPost]
        public ActionResult Post(MgIncome income)
        {
            Console.WriteLine(income.HistId);
            Console.WriteLine(income.HistKode);
            if (income != null)
            {

                var data = _income.SaveDataIncome(income);

                if (data)
                {
                    return Ok();
                }

                return NotFound();
            }


            return NotFound();
        }

       
    }
}
