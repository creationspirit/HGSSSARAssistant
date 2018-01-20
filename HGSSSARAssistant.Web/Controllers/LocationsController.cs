using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HGSSSARAssistant.Core;
using HGSSSARAssistant.Core.Repositories;
using HGSSSARAssistant.Web.Models;

namespace HGSSSARAssistant.Web.Controllers
{
    public class LocationsController : Controller
    {
        private readonly ILocationRepository _context;

        public LocationsController(ILocationRepository context)
        {
            _context = context;
        }

        // GET: Locations
        public ActionResult Index()
        {
            List<LocationViewModel> model = new List<LocationViewModel>();
            foreach (Location l in _context.GetAll())
            {
                LocationViewModel locationModel = ConvertToViewModel(l);
                model.Add(locationModel);
            }

            return View(model);
        }

        // GET: Locations/Details/5
        public ActionResult Details(long id)
        {
            var location = _context.GetById(id);

            if (location == null)
            {
                return NotFound();
            }

            var locationModel = ConvertToViewModel(location);

            return View(locationModel);
        }

        // GET: Locations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Latitude,Longitude,Name,Description,Id")] LocationViewModel locationModel)
        {
            if (ModelState.IsValid)
            {
                var location = ConvertToModel(locationModel);

                _context.Insert(location);
                _context.Save();
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Invalid location data.");
            return View(locationModel);
        }

        // GET: Locations/Edit/5
        public ActionResult Edit(long id)
        {
            var location = _context.GetById(id);

            if (location == null)
            {
                return NotFound();
            }

            LocationViewModel locationModel = ConvertToViewModel(location);

            return View(locationModel);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long id, [Bind("Latitude,Longitude,Name,Description,Id")] LocationViewModel locationModel)
        {
            if (id != locationModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var location = ConvertToModel(locationModel);

                    _context.Update(location);
                    _context.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(locationModel.Id))
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
            return View(locationModel);
        }

        // GET: Locations/Delete/5
        public ActionResult Delete(long id)
        {
            var location = _context.GetById(id);

            if (location == null)
            {
                return NotFound();
            }

            LocationViewModel locationModel = ConvertToViewModel(location);

            return View(locationModel);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            _context.Delete(id);
            _context.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(long id)
        {
            return _context.Exists(id);
        }

        private Location ConvertToModel(LocationViewModel locationModel)
        {
            Location location = new Location
            {
                Id = locationModel.Id,
                Name = locationModel.Name,
                Latitude = locationModel.Latitude,
                Longitude = locationModel.Longitude,
                Description = locationModel.Description
            };

            return location;
        }

        private LocationViewModel ConvertToViewModel(Location location)
        {
            var locationModel = new LocationViewModel
            {
                Id = location.Id,
                Name = location.Name,
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                Description = location.Description
            };

            return locationModel;
        }
    }
}
