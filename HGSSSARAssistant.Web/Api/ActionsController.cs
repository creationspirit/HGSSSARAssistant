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
using HGSSSARAssistant.Core.Repositories;

namespace HGSSSARAssistant.Web.Api
{
    [Produces("application/json")]
    [Route("api/Actions")]
    public class ActionsController : Controller
    {
        private readonly IActionRepository _context;
        private readonly IActionNotifier _notifier;

        public ActionsController(IActionRepository context, IActionNotifier notifier)
        {
            _context = context;
            _notifier = notifier;
        }

        // GET: api/Actions
        [HttpGet]
        public object GetActions()
        {
            IEnumerable<Core.Action> actions = _context.GetAll();

            var result = actions.Select(a => {
                bool isActive = a.ActionType.Name == "Active";

                return new
                {
                    id = a.Id,
                    name = a.Name, 
					description = a.Description,
                    active = isActive,
                    meetupTime = a.MeetupTime,
                    leaderId = a.Leader.Id,
                    location = new {
                        lat = a.Location.Latitude,
                        lng = a.Location.Longitude
                    },
                    invitedRescuers = a.InvitedRescuers.Select(r => r.Id),
                    attendingRescuers = a.AttendedRescuers.Select(r => r.Id),
                };
            });
            return result;
        }

        // GET: api/Actions/5
        [HttpGet("{id}")]
        public ActionResult GetAction([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var action = _context.GetById(id);

            if (action == null)
            {
                return NotFound();
            }

            return Ok(action);
        }

        // PUT: api/Actions/5
        [HttpPut("{id}")]
        public ActionResult PutAction([FromRoute] long id, [FromBody] Core.Action action)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != action.Id)
            {
                return BadRequest();
            }

            try
            {
                _context.Save();
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
        public ActionResult PostAction([FromBody] Core.Action action)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Insert(action);
            _context.Save();
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
        public ActionResult DeleteAction([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_context.Exists(id))
            {
                return NotFound();
            }

            _context.Delete(id);
            _context.Save();

            return Ok();
        }

        private bool ActionExists(long id)
        {
            return _context.Exists(id);
        }
    }
}