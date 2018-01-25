using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HGSSSARAssistant.Web.Models;
using HGSSSARAssistant.Core;
using System.Collections.Generic;
using HGSSSARAssistant.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Linq;

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
        private readonly ILocationRepository _locationRepository;

        public UsersController(
            IUserRepository context,
            IStationRepository stationRepository,
            IRoleRepository roleRepository,
            ICategoryRepository categoryRepository,
            IExpertiseRepository expertiseRepository,
            IAvailabilityRepository availabilityRepository,
            ILocationRepository locationRepository
        )
        {
            _context = context;
            _stationRepository = stationRepository;
            _roleRepository = roleRepository;
            _categoryRepository = categoryRepository;
            _expertiseRepository = expertiseRepository;
            _availabilityRepository = availabilityRepository;
            _locationRepository = locationRepository;
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
        public ActionResult Create([Bind("FirstName,LastName,Email,Address,AddressLat,AddressLng,AndroidPushId,ContactNumber,AdditionalContactNumbers,StationId,CategoryId,RoleId,Id")] UserViewModel userModel)
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
        public ActionResult Edit(long id, [Bind("FirstName,LastName,Email,Address,AddressLat,AddressLng,AndroidPushId,ContactNumber,AdditionalContactNumbers,StationId,CategoryId,RoleId,Id")] UserViewModel userModel)
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

        [HttpGet("[controller]/{id}/Availabilities")]
        public JsonResult UserAvailabilities(long id)
        {
            object result = _context.GetAvailabilitiesByUser(id).Select(a => new {
                title = a.Location.Name,
                start = a.StartTime,
                end = a.EndTime,
                location = new {
                    lat = a.Location.Latitude,
                    lng = a.Location.Longitude
                }
            });
            //List<Availability> availabilities = new List<Availability>(_context.GetAvailabilitiesByUser(id));

            return Json(result);
        }

        // GET: Users/Availability/5
        public ActionResult Availability(long id)
        {
            var user = _context.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            var availabilityModel = new UserAvailabilityViewModel
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Availability = user.Availiabilities ?? new List<Availability>()
            };
           
            return View(availabilityModel);
        }

        [HttpPost("[controller]/Availability/{id}")]
        public ActionResult Availability(long id, [FromBody] UserAvailabilityViewModel viewModel)
        {
            if (id != viewModel.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    User user = _context.GetById(viewModel.UserId);

                    if (user == null) user = new User();
                    if (user.Availiabilities == null) user.Availiabilities = new List<Availability>();
                    else user.Availiabilities.ForEach(a => _availabilityRepository.Delete(a.Id));

                    viewModel.Availability.ForEach(a => {
                        Location location = new Location
                        {
                            Latitude = a.Location.Latitude,
                            Longitude = a.Location.Longitude,
                            Name = a.Location.Name,
                            Description = a.Location.Description
                        };

                        _locationRepository.Insert(location);
                        _locationRepository.Save();

                        Availability availability = new Availability
                        {
                            Day = a.Day,
                            StartTime = a.StartTime,
                            EndTime = a.EndTime,
                            Location = location
                        };

                        _availabilityRepository.Insert(availability);
                        _availabilityRepository.Save();

                        user.Availiabilities.Add(availability);
                    });

                    _context.Update(user);
                    _context.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(viewModel.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok(201);
            }

            return BadRequest();
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

            Location address = user.Address;
            if (user.Address == null) {
                address = _locationRepository.Insert(new Location()
                {
                    Name = userModel.Address,
                    Latitude = userModel.AddressLat,
                    Longitude = userModel.AddressLng
                });
            } else {
                
                address.Name = userModel.Address;
                address.Latitude = userModel.AddressLat;
                address.Longitude = userModel.AddressLng;

                address = _locationRepository.Update(address);
            }

			_locationRepository.Save();
            user.Address = address;

            return user;
        }

        private UserViewModel ConvertToViewModel(User user)
        {
            List<UserExpertiseModel> userExpertises = new List<UserExpertiseModel>();
            foreach (Expertise e in _expertiseRepository.GetAll()) {
                userExpertises.Add(new UserExpertiseModel
                {
                    ExpertiseId = e.Id,
                    ExpertiseName = e.Name,
                    Selected = user.UserExpertise.Exists(ue => ue.ExpertiseId == e.Id)
                });
            }

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
                RoleName = user.Role.Name,
                Address = user.Address.Name,
                AddressLat = user.Address.Latitude,
                AddressLng = user.Address.Longitude,
                Expertise = userExpertises
            };

            return userModel;
        }
    }
}
