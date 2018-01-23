using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HGSSSARAssistant.Core;
using HGSSSARAssistant.Core.Repositories;
using System.Collections.Generic;
using HGSSSARAssistant.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace HGSSSARAssistant.Web.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private readonly IRoleRepository _context;

        public RolesController(IRoleRepository context)
        {
            _context = context;
        }

        // GET: Roles
        public ActionResult Index()
        {
            List<RoleViewModel> model = new List<RoleViewModel>();
            foreach (Role r in _context.GetAll())
            {
                RoleViewModel roleModel = ConvertToViewModel(r);
                model.Add(roleModel);
            }

            return View(model);
        }

        // GET: Roles/Details/5
        public ActionResult Details(long id)
        {
            var role = _context.GetById(id);

            if (role == null)
            {
                return NotFound();
            }

            RoleViewModel roleModel = ConvertToViewModel(role);

            return View(roleModel);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name,Id")] RoleViewModel roleModel)
        {
            if (ModelState.IsValid)
            {
                var role = ConvertToModel(roleModel);

                _context.Insert(role);
                _context.Save();

                return RedirectToAction(nameof(Index));
            }
            return View(roleModel);
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(long id)
        {
            var role = _context.GetById(id);

            if (role == null)
            {
                return NotFound();
            }

            RoleViewModel roleModel = ConvertToViewModel(role);

            return View(roleModel);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long id, [Bind("Name,Id")] RoleViewModel roleModel)
        {
            if (id != roleModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var role = ConvertToModel(roleModel);

                    _context.Update(role);
                    _context.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleExists(roleModel.Id))
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
            return View(roleModel);
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(long id)
        {
            var role = _context.GetById(id);

            if (role == null)
            {
                return NotFound();
            }

            RoleViewModel roleModel = ConvertToViewModel(role);

            return View(roleModel);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            _context.Delete(id);
            _context.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool RoleExists(long id)
        {
            return _context.Exists(id);
        }
        private Role ConvertToModel(RoleViewModel roleModel)
        {
            Role role = new Role
            {
                Id = roleModel.Id,
                Name = roleModel.Name
            };

            return role;
        }

        private RoleViewModel ConvertToViewModel(Role role)
        {
            var roleModel = new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name
            };

            return roleModel;
        }
    }
}
