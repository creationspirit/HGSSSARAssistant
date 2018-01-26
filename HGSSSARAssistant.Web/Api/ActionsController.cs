using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HGSSSARAssistant.Core;
using HGSSSARAssistant.DAL.EF;
using HGSSSARAssistant.Web.Services;

namespace HGSSSARAssistant.Web.Api
{
    [Produces("application/json")]
    [Route("api/Actions")]
    public class ActionsController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IActionNotifier _notifier;

        public ActionsController(ApplicationContext context, ActionPushNotifier notifier)
        {
            _context = context;
            _notifier = notifier;
        }

        // GET: api/Actions
        [HttpGet]
        public IEnumerable<Core.Action> GetActions()
        {
            return _context.Actions;
        }

        // GET: api/Actions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAction([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var action = await _context.Actions.SingleOrDefaultAsync(m => m.Id == id);

            if (action == null)
            {
                return NotFound();
            }

            return Ok(action);
        }

        // PUT: api/Actions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAction([FromRoute] long id, [FromBody] Core.Action action)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != action.Id)
            {
                return BadRequest();
            }

            _context.Entry(action).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActionExists(id))
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

        // POST: api/Actions
        [HttpPost]
        public async Task<IActionResult> PostAction([FromBody] Core.Action action)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Actions.Add(action);
            await _context.SaveChangesAsync();
            foreach(User u in action.InvitedRescuers) {
                try {
					this._notifier.SendNotification(u, "");
                } catch(Exception e) {
                    // User does not have an app bound to his account
                }
            }
            return CreatedAtAction("GetAction", new { id = action.Id }, action);
        }

        // DELETE: api/Actions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAction([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var action = await _context.Actions.SingleOrDefaultAsync(m => m.Id == id);
            if (action == null)
            {
                return NotFound();
            }

            _context.Actions.Remove(action);
            await _context.SaveChangesAsync();

            return Ok(action);
        }

        private bool ActionExists(long id)
        {
            return _context.Actions.Any(e => e.Id == id);
        }
    }
}