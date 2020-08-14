
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
    public class EnrollmentController : ControllerBase
    {
        public EnrollmentController()
        {
        }

        // GET api/enrollment
        [HttpGet("")]
        public ActionResult<IEnumerable<Enrollment>> GetEnrollments()
        {
            return new List<Enrollment> { };
        }

        // GET api/enrollment/5
        [HttpGet("{id}")]
        public ActionResult<Enrollment> GetEnrollmentById(int id)
        {
            return null;
        }

        // POST api/enrollment
        [HttpPost("")]
        public void PostEnrollment(Enrollment value)
        {
        }

        // PUT api/enrollment/5
        [HttpPut("{id}")]
        public void PutEnrollment(int id, Enrollment value)
        {
        }

        // DELETE api/enrollment/5
        [HttpDelete("{id}")]
        public void DeleteEnrollmentById(int id)
        {
        }
    }
}