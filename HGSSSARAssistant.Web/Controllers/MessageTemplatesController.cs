using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HGSSSARAssistant.Core;
using HGSSSARAssistant.DAL.EF;
using HGSSSARAssistant.Core.Repositories;
using HGSSSARAssistant.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace HGSSSARAssistant.Web.Controllers
{
    [Authorize]
    public class MessageTemplatesController : Controller
    {
        private readonly IMessageTemplateRepository _context;

        public MessageTemplatesController(IMessageTemplateRepository context)
        {
            _context = context;
        }

        // GET: MessageTemplates
        public ActionResult Index()
        {
            List<MessageTemplateViewModel> model = new List<MessageTemplateViewModel>();
            foreach (MessageTemplate mt in _context.GetAll())
            {
                MessageTemplateViewModel messageTemplateModel = ConvertToViewModel(mt);
                model.Add(messageTemplateModel);
            }

            return View(model);
        }

        // GET: MessageTemplates/Details/5
        public ActionResult Details(long id)
        {
            var messageTemplate = _context.GetById(id);

            if (messageTemplate == null)
            {
                return NotFound();
            }

            var messageTemplateModel = ConvertToViewModel(messageTemplate);

            return View(messageTemplateModel);
        }

        // GET: MessageTemplates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MessageTemplates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Message,Id")] MessageTemplateViewModel messageTemplateModel)
        {
            if (ModelState.IsValid)
            {
                var messageTemplate = ConvertToModel(messageTemplateModel);

                _context.Insert(messageTemplate);
                _context.Save();
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Invalid messageTemplate data.");
            return View(messageTemplateModel);
        }

        // GET: MessageTemplates/Edit/5
        public ActionResult Edit(long id)
        {
            var messageTemplate = _context.GetById(id);

            if (messageTemplate == null)
            {
                return NotFound();
            }

            MessageTemplateViewModel messageTemplateModel = ConvertToViewModel(messageTemplate);

            return View(messageTemplateModel);
        }

        // POST: MessageTemplates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long id, [Bind("Message,Id")] MessageTemplateViewModel messageTemplateModel)
        {
            if (id != messageTemplateModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var messageTemplate = ConvertToModel(messageTemplateModel);

                    _context.Update(messageTemplate);
                    _context.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageTemplateExists(messageTemplateModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(messageTemplateModel);
        }

        // GET: MessageTemplates/Delete/5
        public ActionResult Delete(long id)
        {
            var messageTemplate = _context.GetById(id);

            if (messageTemplate == null)
            {
                return NotFound();
            }

            MessageTemplateViewModel messageTemplateModel = ConvertToViewModel(messageTemplate);

            return View(messageTemplateModel);
        }

        // POST: MessageTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            _context.Delete(id);
            _context.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageTemplateExists(long id)
        {
            return _context.Exists(id);
        }

        private MessageTemplate ConvertToModel(MessageTemplateViewModel messageTemplateModel)
        {
            MessageTemplate messageTemplate = new MessageTemplate
            {
                Id = messageTemplateModel.Id,
                Message = messageTemplateModel.Message
            };

            return messageTemplate;
        }

        private MessageTemplateViewModel ConvertToViewModel(MessageTemplate messageTemplate)
        {
            var messageTemplateModel = new MessageTemplateViewModel
            {
                Id = messageTemplate.Id,
                Message = messageTemplate.Message
            };

            return messageTemplateModel;
        }
    }
}
