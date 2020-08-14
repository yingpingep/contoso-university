
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
    public class OfficeAssignmentController : ControllerBase
    {
        public OfficeAssignmentController()
        {
        }

        // GET api/officeassignment
        [HttpGet("")]
        public ActionResult<IEnumerable<OfficeAssignment>> GetOfficeAssignments()
        {
            return new List<OfficeAssignment> { };
        }

        // GET api/officeassignment/5
        [HttpGet("{id}")]
        public ActionResult<OfficeAssignment> GetOfficeAssignmentById(int id)
        {
            return null;
        }

        // POST api/officeassignment
        [HttpPost("")]
        public void PostOfficeAssignment(OfficeAssignment value)
        {
        }

        // PUT api/officeassignment/5
        [HttpPut("{id}")]
        public void PutOfficeAssignment(int id, OfficeAssignment value)
        {
        }

        // DELETE api/officeassignment/5
        [HttpDelete("{id}")]
        public void DeleteOfficeAssignmentById(int id)
        {
        }
    }
}