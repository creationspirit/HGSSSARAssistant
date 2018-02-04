using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HGSSSARAssistant.Core;
using HGSSSARAssistant.DAL.EF;
using HGSSSARAssistant.Core.Repositories;

namespace HGSSSARAssistant.Web.Api
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _context;

        public UsersController(IUserRepository context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public object GetUsers()
        {
            DateTime timestamp = DateTime.Now;
            IEnumerable<User> availableUsers = _context.GetAvailableUsers(new DateTime());
            IEnumerable<User> users = _context.GetAll();

            var result = users.Select(u => {
                Location loc = u.GetLocationAtTime(timestamp);

                return new
                {
                    id = u.Id,
                    name = u.FirstName + " " + u.LastName,
                    isAvailable = u.IsAvailable(timestamp),
                    lat = loc.Latitude,
                    lon = loc.Longitude
                };
            });
            return result;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public object GetUser([FromRoute] long id)
        {
            DateTime timestamp = DateTime.Now;
            User user = _context.GetById(id);

            Location loc = user.GetLocationAtTime(timestamp);

            return new
            {
                id = user.Id,
                name = user.FirstName + " " + user.LastName,
                category = user.Category.Name,
                isAvailable = user.IsAvailable(timestamp),
                address = loc.Name,
                contactNumber = user.ContactNumber,
                secondaryContactNumber = user.AdditionalContactNumbers,
                lat = loc.Latitude,
                lon = loc.Longitude
            };
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public ActionResult PutUser([FromRoute] long id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            try
            {
                User result = _context.Update(user);
                _context.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [HttpPost]
        public ActionResult PostUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Insert(user);
            _context.Save();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public ActionResult DeleteUser([FromRoute] long id)
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

        private bool UserExists(long id)
        {
            return _context.Exists(id);
        }
    }
}