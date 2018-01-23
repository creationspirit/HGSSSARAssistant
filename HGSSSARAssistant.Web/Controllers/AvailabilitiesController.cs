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
    public class AvailabilitiesController : Controller
    {
        private readonly IAvailabilityRepository _context;

        public AvailabilitiesController(IAvailabilityRepository context)
        {
            _context = context;
        }

        // GET: Availabilities
        public ActionResult Index()
        {
            List<AvailabilityViewModel> model = new List<AvailabilityViewModel>();
            foreach (Availability a in _context.GetAll())
            {
                AvailabilityViewModel availabilityModel = ConvertToViewModel(a);
                model.Add(availabilityModel);
            }

            return View(model);
        }

        // GET: Availabilities/Details/5
        public ActionResult Details(long id)
        {
            var availability = _context.GetById(id);

            if (availability == null)
            {
                return NotFound();
            }

            var availabilityModel = ConvertToViewModel(availability);

            return View(availabilityModel);
        }

        // GET: Availabilities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Availabilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("StartTime,EndTime,Id")] AvailabilityViewModel availabilityModel)
        {
            if (ModelState.IsValid)
            {
                var availability = ConvertToModel(availabilityModel);

                _context.Insert(availability);
                _context.Save();
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Invalid availability data.");
            return View(availabilityModel);
        }

        // GET: Availabilities/Edit/5
        public ActionResult Edit(long id)
        {
            var availability = _context.GetById(id);

            if (availability == null)
            {
                return NotFound();
            }

            AvailabilityViewModel availabilityModel = ConvertToViewModel(availability);

            return View(availabilityModel);
        }

        // POST: Availabilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long id, [Bind("StartTime,EndTime,Id")] AvailabilityViewModel availabilityModel)
        {
            if (id != availabilityModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var availability = ConvertToModel(availabilityModel);

                    _context.Update(availability);
                    _context.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvailabilityExists(availabilityModel.Id))
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
            return View(availabilityModel);
        }

        // GET: Availabilities/Delete/5
        public ActionResult Delete(long id)
        {
            var availability = _context.GetById(id);

            if (availability == null)
            {
                return NotFound();
            }

            AvailabilityViewModel availabilityModel = ConvertToViewModel(availability);

            return View(availabilityModel);
        }

        // POST: Availabilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            _context.Delete(id);
            _context.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool AvailabilityExists(long id)
        {
            return _context.Exists(id);
        }

        private Availability ConvertToModel(AvailabilityViewModel availabilityModel)
        {
            Availability availability = new Availability
            {
                Id = availabilityModel.Id,
                StartTime = availabilityModel.StartTime,
                EndTime = availabilityModel.EndTime
            };

            return availability;
        }

        private AvailabilityViewModel ConvertToViewModel(Availability availability)
        {
            var availabilityModel = new AvailabilityViewModel
            {
                Id = availability.Id,
                StartTime = availability.StartTime,
                EndTime = availability.EndTime
            };

            return availabilityModel;
        }
    }
}
