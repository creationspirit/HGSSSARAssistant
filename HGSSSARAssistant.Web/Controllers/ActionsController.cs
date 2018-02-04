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
    public class ActionsController : Controller
    {
        private readonly IActionRepository _context;

        public ActionsController(IActionRepository context)
        {
            _context = context;
        }

        // GET: Actions
        public ActionResult Index()
        {
            List<ActionViewModel> model = new List<ActionViewModel>();
            foreach (Core.Action a in _context.GetAll())
            {
                ActionViewModel actionModel = ConvertToViewModel(a);
                model.Add(actionModel);
            }

            return View(model);
        }

        // GET: Actions/Details/5
        public ActionResult Details(long id)
        {
            var action = _context.GetById(id);

            if (action == null)
            {
                return NotFound();
            }

            var actionModel = ConvertToViewModel(action);

            return View(actionModel);
        }

        // GET: Actions/Delete/5
        public ActionResult Delete(long id)
        {
            var action = _context.GetById(id);

            if (action == null)
            {
                return NotFound();
            }

            ActionViewModel actionModel = ConvertToViewModel(action);

            return View(actionModel);
        }

        // POST: Actions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            _context.Delete(id);
            _context.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool ActionExists(long id)
        {
            return _context.Exists(id);
        }

        private Core.Action ConvertToModel(ActionViewModel actionModel)
        {
            Core.Action action = new Core.Action
            {
                Id = actionModel.Id,
                Name = actionModel.Name,
                Description = actionModel.Description,
                MeetupTime = actionModel.MeetupTime
            };

            return action;
        }

        private ActionViewModel ConvertToViewModel(Core.Action action)
        {
            var actionModel = new ActionViewModel
            {
                Id = action.Id,
                Name = action.Name,
                Description  = action.Description,
                MeetupTime = action.MeetupTime
            };

            return actionModel;
        }
    }
}
