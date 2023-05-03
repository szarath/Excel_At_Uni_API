using Excel_At_Uni_API.Data;
using Excel_At_Uni_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excel_At_Uni_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ExcelAtUniDbContext _context;

        /// <summary>
        /// Constructor for StudentsController class.
        /// </summary>
        /// <param name="context">The ExcelAtUniDbContext object.</param>
        /// <returns>
        /// A StudentsController object.
        /// </returns>
        public StudentsController(ExcelAtUniDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a list of all students from the database.
        /// </summary>
        /// <returns>A list of all students.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        /// <summary>
        /// Retrieves a student from the database with the given id.
        /// </summary>
        /// <param name="id">The id of the student to retrieve.</param>
        /// <returns>The student with the given id, or a NotFound result
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        /// <summary>
        /// Updates a student in the database with the given id.
        /// </summary>
        /// <param name="id">The id of the student to update.</param>
        /// <param name="student">The student object to update.</param>
        /// <returns>NoContent if successful, BadRequest if the id does not match the student, NotFound if the student does not exist.</returns>
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.StudentId)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Students
        /// <summary>
        /// Creates a new student and adds it to the database.
        /// </summary>
        /// <param name="student">The student to be added.</param>
        /// <returns>The created student.</returns>
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.StudentId }, student);
        }

        // DELETE: api/Students/5
        /// <summary>
        /// Deletes a student from the database.
        /// </summary>
        /// <param name="id">The id of the student to delete.</param>
        /// <returns>NoContent if the student was successfully deleted, NotFound if the student was not found.</returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Checks if a student with the given ID exists in the database.
        /// </summary>
        /// <param name="id">The ID of the student to check.</param>
        /// <returns>True if the student exists, false otherwise.</returns>
        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
    }
}
