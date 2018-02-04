using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HGSSSARAssistant.Core;
using HGSSSARAssistant.Core.Repositories;
using HGSSSARAssistant.DAL.EF;
using HGSSSARAssistant.DAL;
using HGSSSARAssistant.Web.Models;

namespace HGSSSARAssistant.Web.Controllers
{
    public class ActionTypesController : Controller
    {
        private readonly IActionTypeRepository _context;

        public ActionTypesController(IActionTypeRepository context)
        {
            _context = context;
        }

        // GET: ActionTypes
        public IActionResult Index()
        {
            List<ActionTypeViewModel> model = new List<ActionTypeViewModel>();
            foreach (ActionType at in _context.GetAll())
            {
                ActionTypeViewModel actionModel = ConvertToViewModel(at);
                model.Add(actionModel);
            }

            return View(model);
        }

        // GET: ActionTypes/Details/5
        public ActionResult Details(long id)
        {
            var action = _context.GetById(id);

            if (action == null)
            {
                return NotFound();
            }

            var actionTypeModel = ConvertToViewModel(action);

            return View(actionTypeModel);
        }

        // GET: ActionTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ActionTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name,Id")] ActionTypeViewModel actionTypeModel)
        {
            if (ModelState.IsValid)
            {
                var action = ConvertToModel(actionTypeModel);

                _context.Insert(action);
                _context.Save();
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Invalid action type data.");
            return View(actionTypeModel);
        }

        // GET: ActionTypes/Edit/5
        public ActionResult Edit(long id)
        {
            var actionType = _context.GetById(id);

            if (actionType == null)
            {
                return NotFound();
            }

            ActionTypeViewModel actionModel = ConvertToViewModel(actionType);

            return View(actionModel);
        }

        // POST: ActionTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long id, [Bind("Name,Id")] ActionTypeViewModel actionTypeModel)
        {
            if (id != actionTypeModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var action = ConvertToModel(actionTypeModel);

                    _context.Update(action);
                    _context.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this._context.Exists(actionTypeModel.Id))
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
            return View(actionTypeModel);
        }

        // GET: ActionTypes/Delete/5
        public ActionResult Delete(long id)
        {
            var action = _context.GetById(id);

            if (action == null)
            {
                return NotFound();
            }

            ActionTypeViewModel actionModel = ConvertToViewModel(action);

            return View(actionModel);
        }

        // POST: ActionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            _context.Delete(id);
            _context.Save();
            return RedirectToAction(nameof(Index));
        }

        private ActionType ConvertToModel(ActionTypeViewModel actionModel)
        {
            ActionType action = new ActionType
            {
                Id = actionModel.Id,
                Name = actionModel.Name
            };

            return action;
        }

        private ActionTypeViewModel ConvertToViewModel(ActionType action)
        {
            var actionModel = new ActionTypeViewModel
            {
                Id = action.Id,
                Name = action.Name
            };

            return actionModel;
        }
    }
}
