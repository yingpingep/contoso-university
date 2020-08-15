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
    public class CourseController : ControllerBase
    {
        private readonly ContosoUniversityContext _context;

        public CourseController(ContosoUniversityContext context)
        {
            _context = context;
        }

        // GET api/course
        [HttpGet("")]
        public ActionResult<IEnumerable<Course>> GetCourses()
        {
            return _context.Course.ToArray();
        }

        // GET api/course/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Course>> GetCourseById(int id)
        {
            var course = await _context.Course.FindAsync(id);

            if (course == null) {
                return NotFound();
            }

            return course;
        }

        // POST api/course
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public ActionResult<Course> PostCourse(Course course)
        {
            _context.Course.Add(course);
            _context.SaveChanges();

            return CreatedAtAction("GetCourseById", new { id = course.CourseId }, course);
        }

        // PUT api/course/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult PutCourse(int id, Course patchCourse)
        {
            if (id != patchCourse.CourseId) {
                return BadRequest();
            }

            _context.Entry(patchCourse).State = EntityState.Modified;

            try
            {
                if (CheckCourseExist(id)) {
                    return NotFound();
                }
                
                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                throw;
            }
            return Ok();
        }

        // DELETE api/course/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteCourseById(int id)
        {
            var course = await _context.Course.FindAsync(id);
            if (course == null) {
                return NotFound();
            }

            _context.Remove(course).State = EntityState.Deleted;
            
            try
            {
                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                if (CheckCourseExist(id)) {
                    return NotFound();
                }
                throw;
            }
            return Ok();
        }

        // GET api/course/
        [HttpGet("vw/students")]
        public ActionResult<IEnumerable<VwCourseStudents>> GetCourseStudents()
        {
            return _context.VwCourseStudents.ToArray();
        }

        // GET api/course/
        [HttpGet("vw/studentcount")]
        public ActionResult<IEnumerable<VwCourseStudentCount>> GetCourseStudentCount()
        {
            return _context.VwCourseStudentCount.ToArray();
        }

        private bool CheckCourseExist(int id) {
            return _context.Course.Any(course => course.CourseId == id);
        }
    }
}