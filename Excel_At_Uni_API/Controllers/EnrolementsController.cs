using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Excel_At_Uni_API.Data;
using Excel_At_Uni_API.Models;
using Microsoft.AspNetCore.Authorization;

namespace Excel_At_Uni_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrolmentsController : ControllerBase
    {
        private readonly ExcelAtUniDbContext _context;

        /// <summary>
        /// Constructor for EnrolmentsController class.
        /// </summary>
        /// <param name="context">The ExcelAtUniDbContext object.</param>
        /// <returns>
        /// No return value.
        /// </returns>
        public EnrolmentsController(ExcelAtUniDbContext context)
        {
            _context = context;
        }

        // GET: api/Enrolments
        /// <summary>
        /// Gets a list of all enrolments.
        /// </summary>
        /// <returns>A list of all enrolments.</returns>
        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<IEnumerable<Enrolment>>> GetEnrolments()
        {
            return await _context.Enrolments.ToListAsync();
        }

        // GET: api/Enrolments/5
        /// <summary>
        /// Gets an enrolment by its ID.
        /// </summary>
        /// <param name="id">The ID of the enrolment.</param>
        /// <returns>The enrolment with the specified ID.</returns>
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<Enrolment>> GetEnrolment(int id)
        {
            var enrolment = await _context.Enrolments.FindAsync(id);

            if (enrolment == null)
            {
                return NotFound();
            }

            return enrolment;
        }


        // POST: api/Enrolments
        /// <summary>
        /// Creates a new enrolment in the database.
        /// </summary>
        /// <param name="enrolment">The enrolment to be created.</param>
        /// <returns>The created enrolment.</returns>
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<Enrolment>> PostEnrolment(Enrolment enrolment)
        {
            _context.Enrolments.Add(enrolment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEnrolment), new { id = enrolment.StudentId }, enrolment);
        }

        // PUT: api/Enrolments/5
        /// <summary>
        /// Updates an existing enrolment in the database.
        /// </summary>
        /// <param name="id">The id of the enrolment to be updated.</param>
        /// <param name="enrolment">The updated enrolment object.</param>
        /// <returns>NoContent if the update was successful, BadRequest if the id does not match the enrolment, NotFound if the enrolment does not exist.</returns>
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> PutEnrolment(int id, Enrolment enrolment)
        {
            if (id != enrolment.EnrolmentId)
            {
                return BadRequest();
            }

            _context.Entry(enrolment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrolmentExists(id))
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

        // DELETE: api/Enrolments/5
        /// <summary>
        /// Deletes an enrolment from the database.
        /// </summary>
        /// <param name="id">The id of the enrolment to delete.</param>
        /// <returns>NoContent if the enrolment was successfully deleted, NotFound if the enrolment was not found.</returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteEnrolment(int id)
        {
            var enrolment = await _context.Enrolments.FindAsync(id);
            if (enrolment == null)
            {
                return NotFound();
            }

            _context.Enrolments.Remove(enrolment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Checks if an enrolment exists in the database.
        /// </summary>
        /// <param name="id">The id of the enrolment to check.</param>
        /// <returns>True if the enrolment exists, false otherwise.</returns>
        private bool EnrolmentExists(int id)
        {
            return _context.Enrolments.Any(e => e.EnrolmentId == id);
        }
    }
}
