using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HGSSSARAssistant.Core;
using HGSSSARAssistant.DAL.EF;

namespace HGSSSARAssistant.Web.Api
{
    [Produces("application/json")]
    [Route("api/Availabilities")]
    public class AvailabilitiesController : Controller
    {
        private readonly ApplicationContext _context;

        public AvailabilitiesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Availabilities
        [HttpGet]
        public IEnumerable<Availability> GetAvailabilities()
        {
            return _context.Availabilities;
        }

        // GET: api/Availabilities/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAvailability([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var availability = await _context.Availabilities.SingleOrDefaultAsync(m => m.Id == id);

            if (availability == null)
            {
                return NotFound();
            }

            return Ok(availability);
        }

        // PUT: api/Availabilities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAvailability([FromRoute] long id, [FromBody] Availability availability)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != availability.Id)
            {
                return BadRequest();
            }

            _context.Entry(availability).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AvailabilityExists(id))
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

        // POST: api/Availabilities
        [HttpPost]
        public async Task<IActionResult> PostAvailability([FromBody] Availability availability)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Availabilities.Add(availability);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAvailability", new { id = availability.Id }, availability);
        }

        // DELETE: api/Availabilities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAvailability([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var availability = await _context.Availabilities.SingleOrDefaultAsync(m => m.Id == id);
            if (availability == null)
            {
                return NotFound();
            }

            _context.Availabilities.Remove(availability);
            await _context.SaveChangesAsync();

            return Ok(availability);
        }

        private bool AvailabilityExists(long id)
        {
            return _context.Availabilities.Any(e => e.Id == id);
        }
    }
}