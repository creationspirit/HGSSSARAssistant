using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HGSSSARAssistant.Core;
using HGSSSARAssistant.Core.Repositories;
using HGSSSARAssistant.Web.Models;

namespace HGSSSARAssistant.Web.Controllers
{
    public class StationsController : Controller
    {
        private readonly IStationRepository _context;

        public StationsController(IStationRepository context)
        {
            _context = context;
        }

        // GET: Stations
        public ActionResult Index()
        {
            List<StationViewModel> model = new List<StationViewModel>();
            foreach (Station s in _context.GetAll())
            {
                StationViewModel stationModel = ConvertToViewModel(s);
                model.Add(stationModel);
            }

            return View(model);
        }

        // GET: Stations/Details/5
        public ActionResult Details(long id)
        {
            var station = _context.GetById(id);

            if (station == null)
            {
                return NotFound();
            }

            var stationModel = ConvertToViewModel(station);

            return View(stationModel);
        }

        // GET: Stations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name,Id")] StationViewModel stationModel)
        {
            if (ModelState.IsValid)
            {
                var station = ConvertToModel(stationModel);

                _context.Insert(station);
                _context.Save();

                return RedirectToAction(nameof(Index));
            }
            return View(stationModel);
        }

        // GET: Stations/Edit/5
        public ActionResult Edit(long id)
        {
            var station = _context.GetById(id);

            if (station == null)
            {
                return NotFound();
            }

            StationViewModel stationModel = ConvertToViewModel(station);

            return View(stationModel);
        }

        // POST: Stations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long id, [Bind("Name,Id")] StationViewModel stationModel)
        {
            if (id != stationModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var station = ConvertToModel(stationModel);

                    _context.Update(station);
                    _context.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StationExists(stationModel.Id))
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
            return View(stationModel);
        }

        // GET: Stations/Delete/5
        public ActionResult Delete(long id)
        {
            var station = _context.GetById(id);

            if (station == null)
            {
                return NotFound();
            }

            StationViewModel stationModel = ConvertToViewModel(station);

            return View(stationModel);
        }

        // POST: Stations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            _context.Delete(id);
            _context.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool StationExists(long id)
        {
            return _context.Exists(id);
        }

        private Station ConvertToModel(StationViewModel stationModel)
        {
            Station station = new Station
            {
                Id = stationModel.Id,
                Name = stationModel.Name
            };

            return station;
        }

        private StationViewModel ConvertToViewModel(Station station)
        {
            var stationModel = new StationViewModel
            {
                Id = station.Id,
                Name = station.Name
            };

            return stationModel;
        }
    }
}
