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
    [Route("api/Expertises")]
    public class ExpertisesController : Controller
    {
        private readonly ApplicationContext _context;

        public ExpertisesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Expertises
        [HttpGet]
        public IEnumerable<Expertise> GetExpertises()
        {
            return _context.Expertises;
        }

        // GET: api/Expertises/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpertise([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var expertise = await _context.Expertises.SingleOrDefaultAsync(m => m.Id == id);

            if (expertise == null)
            {
                return NotFound();
            }

            return Ok(expertise);
        }

        // PUT: api/Expertises/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpertise([FromRoute] long id, [FromBody] Expertise expertise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != expertise.Id)
            {
                return BadRequest();
            }

            _context.Entry(expertise).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpertiseExists(id))
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

        // POST: api/Expertises
        [HttpPost]
        public async Task<IActionResult> PostExpertise([FromBody] Expertise expertise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Expertises.Add(expertise);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExpertise", new { id = expertise.Id }, expertise);
        }

        // DELETE: api/Expertises/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpertise([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var expertise = await _context.Expertises.SingleOrDefaultAsync(m => m.Id == id);
            if (expertise == null)
            {
                return NotFound();
            }

            _context.Expertises.Remove(expertise);
            await _context.SaveChangesAsync();

            return Ok(expertise);
        }

        private bool ExpertiseExists(long id)
        {
            return _context.Expertises.Any(e => e.Id == id);
        }
    }
}