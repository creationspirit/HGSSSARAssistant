using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HGSSSARAssistant.Core;
using HGSSSARAssistant.DAL.EF;
using HGSSSARAssistant.Core.Repositories;
using HGSSSARAssistant.Web.Models;
using System.Collections.Generic;

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
        public ActionResult Index()
        {
            List<ActionTypeViewModel> model = new List<ActionTypeViewModel>();
            foreach (ActionType at in _context.GetAll())
            {
                ActionTypeViewModel actionTypeModel = ConvertToViewModel(at);
                model.Add(actionTypeModel);
            }

            return View(model);
        }

        // GET: ActionTypes/Details/5
        public ActionResult Details(long id)
        {
            var actionType = _context.GetById(id);

            if (actionType == null)
            {
                return NotFound();
            }

            var actionTypeModel = ConvertToViewModel(actionType);

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
        public ActionResult Create([Bind("Id")] ActionTypeViewModel actionTypeModel)
        {
            if (ModelState.IsValid)
            {
                var actionType = ConvertToModel(actionTypeModel);

                _context.Insert(actionType);
                _context.Save();
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Invalid actionType data.");
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

            ActionTypeViewModel actionTypeModel = ConvertToViewModel(actionType);

            return View(actionTypeModel);
        }

        // POST: ActionTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long id, [Bind("Id")] ActionTypeViewModel actionTypeModel)
        {
            if (id != actionTypeModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var actionType = ConvertToModel(actionTypeModel);

                    _context.Update(actionType);
                    _context.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActionTypeExists(actionTypeModel.Id))
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
            var actionType = _context.GetById(id);

            if (actionType == null)
            {
                return NotFound();
            }

            ActionTypeViewModel actionTypeModel = ConvertToViewModel(actionType);

            return View(actionTypeModel);
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

        private bool ActionTypeExists(long id)
        {
            return _context.Exists(id);
        }

        private ActionType ConvertToModel(ActionTypeViewModel actionTypeModel)
        {
            ActionType actionType = new ActionType
            {
                Id = actionTypeModel.Id,
                Name = actionTypeModel.Name
            };

            return actionType;
        }

        private ActionTypeViewModel ConvertToViewModel(ActionType actionType)
        {
            var actionTypeModel = new ActionTypeViewModel
            {
                Id = actionType.Id,
                Name = actionType.Name
            };

            return actionTypeModel;
        }
    }
}
