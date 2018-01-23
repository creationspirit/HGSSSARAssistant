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
    public class ExpertisesController : Controller
    {
        private readonly IExpertiseRepository _context;

        public ExpertisesController(IExpertiseRepository context)
        {
            _context = context;
        }

        // GET: Expertises
        public ActionResult Index()
        {
            List<ExpertiseViewModel> model = new List<ExpertiseViewModel>();
            foreach (Expertise e in _context.GetAll())
            {
                ExpertiseViewModel expertiseModel = ConvertToViewModel(e);
                model.Add(expertiseModel);
            }

            return View(model);
        }

        // GET: Expertises/Details/5
        public ActionResult Details(long id)
        {
            var expertise = _context.GetById(id);

            if (expertise == null)
            {
                return NotFound();
            }

            var expertiseModel = ConvertToViewModel(expertise);

            return View(expertiseModel);
        }

        // GET: Expertises/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Expertises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name,Id")] ExpertiseViewModel expertiseModel)
        {
            if (ModelState.IsValid)
            {
                var expertise = ConvertToModel(expertiseModel);

                _context.Insert(expertise);
                _context.Save();
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Invalid expertise data.");
            return View(expertiseModel);
        }

        // GET: Expertises/Edit/5
        public ActionResult Edit(long id)
        {
            var expertise = _context.GetById(id);

            if (expertise == null)
            {
                return NotFound();
            }

            ExpertiseViewModel expertiseModel = ConvertToViewModel(expertise);

            return View(expertiseModel);
        }

        // POST: Expertises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long id, [Bind("Name,Id")] ExpertiseViewModel expertiseModel)
        {
            if (id != expertiseModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var expertise = ConvertToModel(expertiseModel);

                    _context.Update(expertise);
                    _context.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpertiseExists(expertiseModel.Id))
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
            return View(expertiseModel);
        }

        // GET: Expertises/Delete/5
        public ActionResult Delete(long id)
        {
            var expertise = _context.GetById(id);

            if (expertise == null)
            {
                return NotFound();
            }

            ExpertiseViewModel expertiseModel = ConvertToViewModel(expertise);

            return View(expertiseModel);
        }

        // POST: Expertises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            _context.Delete(id);
            _context.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpertiseExists(long id)
        {
            return _context.Exists(id);
        }

        private Expertise ConvertToModel(ExpertiseViewModel expertiseModel)
        {
            Expertise expertise = new Expertise
            {
                Id = expertiseModel.Id,
                Name = expertiseModel.Name
            };

            return expertise;
        }

        private ExpertiseViewModel ConvertToViewModel(Expertise expertise)
        {
            var expertiseModel = new ExpertiseViewModel
            {
                Id = expertise.Id,
                Name = expertise.Name
            };

            return expertiseModel;
        }
    }
}
