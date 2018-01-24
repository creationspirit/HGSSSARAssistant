using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HGSSSARAssistant.Web.Models;
using HGSSSARAssistant.Core;
using System.Collections.Generic;
using HGSSSARAssistant.Core.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace HGSSSARAssistant.Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserRepository _context;

        private readonly IStationRepository _stationRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IExpertiseRepository _expertiseRepository;
        private readonly IAvailabilityRepository _availabilityRepository;

        public UsersController(
            IUserRepository context,
            IStationRepository stationRepository,
            IRoleRepository roleRepository,
            ICategoryRepository categoryRepository,
            IExpertiseRepository expertiseRepository,
            IAvailabilityRepository availabilityRepository
        )
        {
            _context = context;
            _stationRepository = stationRepository;
            _roleRepository = roleRepository;
            _categoryRepository = categoryRepository;
            _expertiseRepository = expertiseRepository;
            _availabilityRepository = availabilityRepository;
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
            ViewBag.RoleList = new List<Role>(_roleRepository.GetAll());
            ViewBag.CategoryList = new List<Category>(_categoryRepository.GetAll());
            ViewBag.ExpertiseList = new List<Expertise>(_expertiseRepository.GetAll());
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("FirstName,LastName,Email,Address,AndroidPushId,ContactNumber,AdditionalContactNumbers,StationId,CategoryId,RoleId,Id")] UserViewModel userModel)
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

            ViewBag.StationList = new List<Station>(_stationRepository.GetAll());
            ViewBag.RoleList = new List<Role>(_roleRepository.GetAll());
            ViewBag.CategoryList = new List<Category>(_categoryRepository.GetAll());
            ViewBag.ExpertiseList = new List<Expertise>(_expertiseRepository.GetAll());
            return View(userModel);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long id, [Bind("FirstName,LastName,Email,Address,AndroidPushId,ContactNumber,AdditionalContactNumbers,StationId,CategoryId,RoleId,Id")] UserViewModel userModel)
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

        // GET: Users/Availability/5
        public ActionResult Availability(long id)
        {
            var user = _context.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            UserAvailabilityViewModel availabilityModel = ConvertToAvailabilityModel(user);

            return View(availabilityModel);
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
            User user = _context.GetById(userModel.Id);
            if (user == null) user = new User();

            user.Id = userModel.Id;
            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.Email = userModel.Email;
            user.AndroidPushId = userModel.AndroidPushId;
            user.ContactNumber = userModel.ContactNumber;
            user.AdditionalContactNumbers = userModel.AdditionalContactNumbers;
            user.Station = _stationRepository.GetById(userModel.StationId);
            user.Role = _roleRepository.GetById(userModel.RoleId);
            user.Category = _categoryRepository.GetById(userModel.CategoryId);

            return user;
        }

        private UserViewModel ConvertToViewModel(User user)
        {
            var userModel = new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                AndroidPushId = user.AndroidPushId,
                ContactNumber = user.ContactNumber,
                AdditionalContactNumbers = user.AdditionalContactNumbers,
                StationId = user.Station.Id,
                StationName = user.Station.Name,
                CategoryId = user.Category.Id,
                CategoryName = user.Category.Name,
                RoleId = user.Role.Id,
                RoleName = user.Role.Name
            };

            return userModel;
        }

        private UserAvailabilityViewModel ConvertToAvailabilityModel(User user)
        {
            var availabilityModel = new UserAvailabilityViewModel
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            return availabilityModel;
        }
    }
}
