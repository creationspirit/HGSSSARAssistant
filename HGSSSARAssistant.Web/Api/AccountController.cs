using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HGSSSARAssistant.Core.Repositories;
using HGSSSARAssistant.Core;

namespace HGSSSARAssistant.Web.Api
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly IUserRepository _context;

        public AccountController(IUserRepository context)
        {
            _context = context;
        }
        // POST: api/Account
        [HttpPost]
        public ActionResult Post([FromBody] string email, [FromBody] string password)
        {
            var user = _context.GetUserByEmail(email);

            if(user == null)
            {
                return NotFound();
            }

            if (user.Password.Equals(password))
            {
                // TODO: Generate Authentication token
                return Ok("jwt");
            } else
            {
                return Unauthorized();
            }
        }
    }
}
