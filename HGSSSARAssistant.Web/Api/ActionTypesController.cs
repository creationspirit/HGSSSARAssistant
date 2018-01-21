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
    [Route("api/ActionTypes")]
    public class ActionTypesController : Controller
    {
        private readonly ApplicationContext _context;

        public ActionTypesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/ActionTypes
        [HttpGet]
        public IEnumerable<ActionType> GetActionTypes()
        {
            return _context.ActionTypes;
        }

        // GET: api/ActionTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActionType([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actionType = await _context.ActionTypes.SingleOrDefaultAsync(m => m.Id == id);

            if (actionType == null)
            {
                return NotFound();
            }

            return Ok(actionType);
        }

        // PUT: api/ActionTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActionType([FromRoute] long id, [FromBody] ActionType actionType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != actionType.Id)
            {
                return BadRequest();
            }

            _context.Entry(actionType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActionTypeExists(id))
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

        // POST: api/ActionTypes
        [HttpPost]
        public async Task<IActionResult> PostActionType([FromBody] ActionType actionType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ActionTypes.Add(actionType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActionType", new { id = actionType.Id }, actionType);
        }

        // DELETE: api/ActionTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActionType([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actionType = await _context.ActionTypes.SingleOrDefaultAsync(m => m.Id == id);
            if (actionType == null)
            {
                return NotFound();
            }

            _context.ActionTypes.Remove(actionType);
            await _context.SaveChangesAsync();

            return Ok(actionType);
        }

        private bool ActionTypeExists(long id)
        {
            return _context.ActionTypes.Any(e => e.Id == id);
        }
    }
}