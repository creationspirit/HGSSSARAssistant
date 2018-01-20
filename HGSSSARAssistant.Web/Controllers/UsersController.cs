using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HGSSSARAssistant.Web.Models;
using HGSSSARAssistant.Core;
using System.Collections.Generic;
using HGSSSARAssistant.Core.Repositories;

namespace HGSSSARAssistant.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _context;

        private readonly IStationRepository _stationRepository;

        public UsersController(IUserRepository context, IStationRepository stationRepository)
        {
            _context = context;
            _stationRepository = stationRepository;
        }

        // GET: Users
        public ActionResult Index()
        {
            List<UserViewModel> model = new List<UserViewModel>();
            foreach (User u in _context.GetAll())
            {
                UserViewModel userModel = ConvertToViewModel(u);
                model.Add(userModel);
            }

            return View(model);
        }

        // GET: Users/Details/5
        public ActionResult Details(long id)
        {
            var user = _context.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            var userModel = ConvertToViewModel(user);

            return View(userModel);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewBag.StationList = new List<Station>(_stationRepository.GetAll());
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("FirstName,LastName,Address,Email,AndroidPushId,Password,PasswordSalt,ContactNumber,AdditionalContactNumbers,Station,Id")] UserViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                var user = ConvertToModel(userModel);

                _context.Insert(user);
                _context.Save();
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Invalid user data.");
            ViewBag.StationList = new List<Station>(_stationRepository.GetAll());
            return View(userModel);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(long id)
        {
            var user = _context.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            UserViewModel userModel = ConvertToViewModel(user);

            return View(userModel);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long id, [Bind("FirstName,LastName,Address,Email,AndroidPushId,Password,PasswordSalt,ContactNumber,AdditionalContactNumbers,Id")] UserViewModel userModel)
        {
            if (id != userModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = ConvertToModel(userModel);

                    _context.Update(user);
                    _context.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(userModel.Id))
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
            return View(userModel);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(long id)
        {
            var user = _context.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            UserViewModel userModel = ConvertToViewModel(user);

            return View(userModel);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            _context.Delete(id);
            _context.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(long id)
        {
            return _context.Exists(id);
        }

        private User ConvertToModel(UserViewModel userModel)
        {
            User user = new User
            {
                Id = userModel.Id,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Address = userModel.Address,
                Email = userModel.Email,
                AndroidPushId = userModel.AndroidPushId,
                Password = userModel.Password,
                PasswordSalt = userModel.PasswordSalt,
                ContactNumber = userModel.ContactNumber,
                AdditionalContactNumbers = userModel.AdditionalContactNumbers,
                Station = userModel.Station == null ? null : _stationRepository.GetById(userModel.Station.Id)
            };

            return user;
        }

        private UserViewModel ConvertToViewModel(User user)
        {
            var userModel = new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Email = user.Email,
                AndroidPushId = user.AndroidPushId,
                Password = user.Password,
                PasswordSalt = user.PasswordSalt,
                ContactNumber = user.ContactNumber,
                AdditionalContactNumbers = user.AdditionalContactNumbers,
                Station = user.Station == null ? null : new StationViewModel
                {
                    Id = user.Station.Id,
                    Name = user.Station.Name
                }
            };

            return userModel;
        }
    }
}
