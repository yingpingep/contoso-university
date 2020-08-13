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
    public class CourseController : ControllerBase
    {
        public CourseController()
        {
        }

        // GET api/course
        [HttpGet("")]
        public ActionResult<IEnumerable<Course>> GetCourses()
        {
            return new List<Course> { };
        }

        // GET api/course/5
        [HttpGet("{id}")]
        public ActionResult<Course> GetCourseById(int id)
        {
            return null;
        }

        // POST api/course
        [HttpPost("")]
        public void PostCourse(Course value)
        {
        }

        // PUT api/course/5
        [HttpPut("{id}")]
        public void PutCourse(int id, Course value)
        {
        }

        // DELETE api/course/5
        [HttpDelete("{id}")]
        public void DeleteCourseById(int id)
        {
        }
    }
}