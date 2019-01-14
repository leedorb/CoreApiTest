using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Results;
using app.Dal;
using app.Models;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        // GET api/Person
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

        // GET api/Person/5
        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            try
            {
                var person = dbManager.GetPerson(id);

                if (person != null)
                {
                    return Ok(person);
                }

                return  StatusCode(400, "Person id " + id + " Not Found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error - " + ex.Message);
            }
        }

        // POST api/Person
        [HttpPost]
        public ActionResult Post([FromBody] Person person)
        {
            try
            {
                dbManager.InsertNewPerson(person);
                return Ok("Done");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error - " + ex.Message);
            }
        }

        // PUT api/Person/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Person person)
        {
            try
            {
                if (id != person.ID)
                {
                    return StatusCode(400, "Error - ID not match, ID can not be change");
                }

                dbManager.UpdatePerson(person);
                return Ok("Done");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error - " + ex.Message);
            }
        }

        // DELETE api/Person/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                dbManager.DeletePerson(id);
                return Ok("Done");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error - " + ex.Message);
            }
        }        
    }
}
