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
    [Route("api/MessageTemplates")]
    public class MessageTemplatesController : Controller
    {
        private readonly ApplicationContext _context;

        public MessageTemplatesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/MessageTemplates
        [HttpGet]
        public IEnumerable<MessageTemplate> GetMessageTemplates()
        {
            return _context.MessageTemplates;
        }

        // GET: api/MessageTemplates/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessageTemplate([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var messageTemplate = await _context.MessageTemplates.SingleOrDefaultAsync(m => m.Id == id);

            if (messageTemplate == null)
            {
                return NotFound();
            }

            return Ok(messageTemplate);
        }

        // PUT: api/MessageTemplates/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessageTemplate([FromRoute] long id, [FromBody] MessageTemplate messageTemplate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != messageTemplate.Id)
            {
                return BadRequest();
            }

            _context.Entry(messageTemplate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageTemplateExists(id))
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

        // POST: api/MessageTemplates
        [HttpPost]
        public async Task<IActionResult> PostMessageTemplate([FromBody] MessageTemplate messageTemplate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.MessageTemplates.Add(messageTemplate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMessageTemplate", new { id = messageTemplate.Id }, messageTemplate);
        }

        // DELETE: api/MessageTemplates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessageTemplate([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var messageTemplate = await _context.MessageTemplates.SingleOrDefaultAsync(m => m.Id == id);
            if (messageTemplate == null)
            {
                return NotFound();
            }

            _context.MessageTemplates.Remove(messageTemplate);
            await _context.SaveChangesAsync();

            return Ok(messageTemplate);
        }

        private bool MessageTemplateExists(long id)
        {
            return _context.MessageTemplates.Any(e => e.Id == id);
        }
    }
}