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
    public class CourseInstructorController : ControllerBase
    {
        public CourseInstructorController()
        {
        }

        // GET api/courseinstructor
        [HttpGet("")]
        public ActionResult<IEnumerable<CourseInstructor>> GetCourseInstructors()
        {
            return new List<CourseInstructor> { };
        }

        // GET api/courseinstructor/5
        [HttpGet("{id}")]
        public ActionResult<CourseInstructor> GetCourseInstructorById(int id)
        {
            return null;
        }

        // POST api/courseinstructor
        [HttpPost("")]
        public void PostCourseInstructor(CourseInstructor value)
        {
        }

        // PUT api/courseinstructor/5
        [HttpPut("{id}")]
        public void PutCourseInstructor(int id, CourseInstructor value)
        {
        }

        // DELETE api/courseinstructor/5
        [HttpDelete("{id}")]
        public void DeleteCourseInstructorById(int id)
        {
        }
    }
}