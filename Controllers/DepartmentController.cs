using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using contoso_university.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace contoso_university.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ContosoUniversityContext _context;
        public DepartmentController(ContosoUniversityContext context)
        {
            _context = context;
        }

        // GET api/Department/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Department>> GetDepartmentById(int id)
        {
            var Department = await _context.Department.FindAsync(id);

            if (Department == null)
            {
                return NotFound();
            }

            return Department;
        }

        // POST api/department
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Department>> PostDepartment(Department department)
        {
            try
            {
                var items = await _context.Department.FromSqlInterpolated($"EXECUTE Department_Insert {department.Name}, {department.Budget}, {department.StartDate}, {department.InstructorId}").Select(department => department.DepartmentId).ToListAsync();

                department.DepartmentId = items.First();
            }
            catch (System.Exception)
            {
                throw;
            }

            return CreatedAtAction("GetDepartmentById", new { id = department.DepartmentId }, department);
        }

        // PUT api/department/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Department>> PutDepartment(int id, Department department)
        {
            if (id != department.DepartmentId)
            {
                return BadRequest();
            }

            try
            {
                var origin = await _context.Department.Where(d => d.DepartmentId == id).FirstAsync();

                var items = await _context.Department.FromSqlInterpolated($"EXECUTE Department_Update {id}, {department.Name}, {department.Budget}, {department.StartDate}, {department.InstructorId}, {origin.RowVersion}").Select(d => d.RowVersion).ToListAsync();
            }
            catch (System.Exception)
            {
                throw;
            }

            return Ok();
        }

        // DELETE api/department/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteDepartmentById(int id) { 
            if (!_context.Department.Any(d => d.DepartmentId == id))
            {
                return NotFound();
            }

            try
            {
                var origin = await _context.Department.Where(d => d.DepartmentId == id).FirstAsync();

                _context.Department.FromSqlInterpolated($"EXECUTE Department_Delete {id}, {origin.RowVersion}").AsNoTracking();
            }
            catch (System.Exception)
            {
                throw;
            }

            return Ok();
        }

        // GET api/department/
        [HttpGet("vw/coursecount")]
        public ActionResult<IEnumerable<VwDepartmentCourseCount>> GetDepartmentCourseCount()
        {
            return _context.VwDepartmentCourseCount.FromSqlInterpolated($"SELECT * FROM dbo.vwDepartmentCourseCount").ToArray();
        }
    }
}