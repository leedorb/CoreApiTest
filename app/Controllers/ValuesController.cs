using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Dal;
using app.Models;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            try
            {
                var list = dbManager.GetPersonsList();

                if (list.Count != 0)
                {
                    return Ok(list);
                }

                return new string[] { "Error", "db list empty" };
            }
            catch (Exception ex)
            {

                return new string[] { "Error", ex.Message };
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            try
            {
                var person = dbManager.GetPerson(id);

                if (person != null)
                {
                    return Ok(person);
                }

                return "Person " + id + " Not Found";
            }
            catch (Exception ex)
            {
                return "Error - " + ex.Message;
            }
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Person person)
        {
            dbManager.InsertNewPerson(person);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
