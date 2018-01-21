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
    [Route("api/Stations")]
    public class StationsController : Controller
    {
        private readonly ApplicationContext _context;

        public StationsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Stations
        [HttpGet]
        public IEnumerable<Station> GetStations()
        {
            return _context.Stations;
        }

        // GET: api/Stations/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStation([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var station = await _context.Stations.SingleOrDefaultAsync(m => m.Id == id);

            if (station == null)
            {
                return NotFound();
            }

            return Ok(station);
        }

        // PUT: api/Stations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStation([FromRoute] long id, [FromBody] Station station)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != station.Id)
            {
                return BadRequest();
            }

            _context.Entry(station).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StationExists(id))
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

        // POST: api/Stations
        [HttpPost]
        public async Task<IActionResult> PostStation([FromBody] Station station)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Stations.Add(station);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStation", new { id = station.Id }, station);
        }

        // DELETE: api/Stations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStation([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var station = await _context.Stations.SingleOrDefaultAsync(m => m.Id == id);
            if (station == null)
            {
                return NotFound();
            }

            _context.Stations.Remove(station);
            await _context.SaveChangesAsync();

            return Ok(station);
        }

        private bool StationExists(long id)
        {
            return _context.Stations.Any(e => e.Id == id);
        }
    }
}