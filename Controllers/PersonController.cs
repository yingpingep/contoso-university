using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using contoso_university.Models;

namespace contoso_university.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        public PersonController()
        {
        }

        // GET api/person
        [HttpGet("")]
        public ActionResult<IEnumerable<Person>> GetPersons()
        {
            return new List<Person> { };
        }

        // GET api/person/5
        [HttpGet("{id}")]
        public ActionResult<Person> GetPersonById(int id)
        {
            return null;
        }

        // POST api/person
        [HttpPost("")]
        public void PostPerson(Person value)
        {
        }

        // PUT api/person/5
        [HttpPut("{id}")]
        public void PutPerson(int id, Person value)
        {
        }

        // DELETE api/person/5
        [HttpDelete("{id}")]
        public void DeletePersonById(int id)
        {
        }
    }
}
