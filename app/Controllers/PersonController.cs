﻿using app.Dal;
using app.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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

                return Ok(list);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error - " + ex.Message);
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

                return StatusCode(400, "Person not Found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error - " + ex.Message);
            }
        }

        // POST api/Person
        [HttpPost]
        public void Post([FromBody] Person person)
        {
            dbManager.InsertNewPerson(person);
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
        public void Delete(string id)
        {
            dbManager.DeletePerson(id);
        }
    }
}
