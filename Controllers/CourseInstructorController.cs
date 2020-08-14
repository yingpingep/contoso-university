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
    public class CourseInstructorController : ControllerBase
    {
        private readonly ContosoUniversityContext _context;

        public CourseInstructorController(ContosoUniversityContext context)
        {
            _context = context;
        }

        // GET api/CourseInstructor
        [HttpGet("")]
        public ActionResult<IEnumerable<CourseInstructor>> GetCourseInstructors()
        {
            return _context.CourseInstructor.ToArray();
        }

        // GET api/CourseInstructor/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<CourseInstructor>> GetCourseInstructorById(int id)
        {
            var CourseInstructor = await _context.CourseInstructor.FindAsync(id);

            if (CourseInstructor == null) {
                return NotFound();
            }

            return CourseInstructor;
        }

        // POST api/CourseInstructor
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<CourseInstructor>> PostCourseInstructor(CourseInstructor CourseInstructor)
        {
            _context.CourseInstructor.Add(CourseInstructor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourseInstructorById", new { id = CourseInstructor.CourseId }, CourseInstructor);
        }

        // PUT api/CourseInstructor/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutCourseInstructor(int id, CourseInstructor patchCourseInstructor)
        {
            if (id != patchCourseInstructor.CourseId) {
                return BadRequest();
            }

            _context.Entry(patchCourseInstructor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                if (CheckCourseInstructorExist(id)) {
                    return NotFound();
                }
                throw;
            }
            return Ok();
        }

        // DELETE api/CourseInstructor/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteCourseInstructorById(int id)
        {
            var CourseInstructor = await _context.CourseInstructor.FindAsync(id);
            if (CourseInstructor == null) {
                return NotFound();
            }

            _context.Remove(CourseInstructor).State = EntityState.Deleted;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                if (CheckCourseInstructorExist(id)) {
                    return NotFound();
                }
                throw;
            }
            return Ok();
        }

        private bool CheckCourseInstructorExist(int id) {
            return _context.CourseInstructor.Any(CourseInstructor => CourseInstructor.CourseId == id);
        }
    }
}