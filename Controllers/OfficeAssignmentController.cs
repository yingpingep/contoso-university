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
    public class OfficeAssignmentController : ControllerBase
    {
        private readonly ContosoUniversityContext _context;

        public OfficeAssignmentController(ContosoUniversityContext context)
        {
            _context = context;
        }

        // GET api/OfficeAssignment
        [HttpGet("")]
        public ActionResult<IEnumerable<OfficeAssignment>> GetOfficeAssignments()
        {
            return _context.OfficeAssignment.ToArray();
        }

        // GET api/OfficeAssignment/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<OfficeAssignment>> GetOfficeAssignmentById(int id)
        {
            var OfficeAssignment = await _context.OfficeAssignment.FindAsync(id);

            if (OfficeAssignment == null) {
                return NotFound();
            }

            return OfficeAssignment;
        }

        // POST api/OfficeAssignment
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<OfficeAssignment>> PostOfficeAssignment(OfficeAssignment OfficeAssignment)
        {
            _context.OfficeAssignment.Add(OfficeAssignment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOfficeAssignmentById", new { id = OfficeAssignment.InstructorId }, OfficeAssignment);
        }

        // PUT api/OfficeAssignment/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutOfficeAssignment(int id, OfficeAssignment patchOfficeAssignment)
        {
            if (id != patchOfficeAssignment.InstructorId) {
                return BadRequest();
            }

            _context.Entry(patchOfficeAssignment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                if (CheckOfficeAssignmentExist(id)) {
                    return NotFound();
                }
                throw;
            }
            return Ok();
        }

        // DELETE api/OfficeAssignment/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteOfficeAssignmentById(int id)
        {
            var OfficeAssignment = await _context.OfficeAssignment.FindAsync(id);
            if (OfficeAssignment == null) {
                return NotFound();
            }

            _context.Remove(OfficeAssignment).State = EntityState.Deleted;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                if (CheckOfficeAssignmentExist(id)) {
                    return NotFound();
                }
                throw;
            }
            return Ok();
        }

        private bool CheckOfficeAssignmentExist(int id) {
            return _context.OfficeAssignment.Any(OfficeAssignment => OfficeAssignment.InstructorId == id);
        }
    }
}