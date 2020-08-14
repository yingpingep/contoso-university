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
    public class EnrollmentController : ControllerBase
    {
        private readonly ContosoUniversityContext _context;

        public EnrollmentController(ContosoUniversityContext context)
        {
            _context = context;
        }

        // GET api/Enrollment
        [HttpGet("")]
        public ActionResult<IEnumerable<Enrollment>> GetEnrollments()
        {
            return _context.Enrollment.ToArray();
        }

        // GET api/Enrollment/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Enrollment>> GetEnrollmentById(int id)
        {
            var Enrollment = await _context.Enrollment.FindAsync(id);

            if (Enrollment == null) {
                return NotFound();
            }

            return Enrollment;
        }

        // POST api/Enrollment
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Enrollment>> PostEnrollment(Enrollment Enrollment)
        {
            _context.Enrollment.Add(Enrollment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEnrollmentById", new { id = Enrollment.EnrollmentId }, Enrollment);
        }

        // PUT api/Enrollment/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutEnrollment(int id, Enrollment patchEnrollment)
        {
            if (id != patchEnrollment.EnrollmentId) {
                return BadRequest();
            }

            _context.Entry(patchEnrollment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                if (CheckEnrollmentExist(id)) {
                    return NotFound();
                }
                throw;
            }
            return Ok();
        }

        // DELETE api/Enrollment/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteEnrollmentById(int id)
        {
            var Enrollment = await _context.Enrollment.FindAsync(id);
            if (Enrollment == null) {
                return NotFound();
            }

            _context.Remove(Enrollment).State = EntityState.Deleted;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                if (CheckEnrollmentExist(id)) {
                    return NotFound();
                }
                throw;
            }
            return Ok();
        }

        private bool CheckEnrollmentExist(int id) {
            return _context.Enrollment.Any(Enrollment => Enrollment.EnrollmentId == id);
        }
    }
}