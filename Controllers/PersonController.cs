using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using contoso_university.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace contoso_university.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ContosoUniversityContext _context;

        public PersonController(ContosoUniversityContext context)
        {
            _context = context;
        }

        // GET api/Person
        [HttpGet("")]
        public ActionResult<IEnumerable<Person>> GetPersons()
        {
            return _context.Person.ToArray();
        }

        // GET api/Person/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Person>> GetPersonById(int id)
        {
            var Person = await _context.Person.FindAsync(id);

            if (Person == null) {
                return NotFound();
            }

            return Person;
        }

        // POST api/Person
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Person>> PostPerson(Person Person)
        {
            _context.Person.Add(Person);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonById", new { id = Person.Id }, Person);
        }

        // PUT api/Person/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutPerson(int id, Person patchPerson)
        {
            if (id != patchPerson.Id) {
                return BadRequest();
            }

            _context.Entry(patchPerson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                if (CheckPersonExist(id)) {
                    return NotFound();
                }
                throw;
            }
            return Ok();
        }

        // DELETE api/Person/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeletePersonById(int id)
        {
            var Person = await _context.Person.FindAsync(id);
            if (Person == null) {
                return NotFound();
            }

            _context.Remove(Person).State = EntityState.Deleted;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                if (CheckPersonExist(id)) {
                    return NotFound();
                }
                throw;
            }
            return Ok();
        }

        private bool CheckPersonExist(int id) {
            return _context.Person.Any(Person => Person.Id == id);
        }
    }
}