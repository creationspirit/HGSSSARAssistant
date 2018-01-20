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
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _context;

        public CategoriesController(ICategoryRepository context)
        {
            _context = context;
        }

        // GET: Categories
        public ActionResult Index()
        {
            List<CategoryViewModel> model = new List<CategoryViewModel>();
            foreach (Category c in _context.GetAll())
            {
                CategoryViewModel categoryModel = ConvertToViewModel(c);
                model.Add(categoryModel);
            }

            return View(model);
        }

        // GET: Categories/Details/5
        public ActionResult Details(long id)
        {
            var category = _context.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryModel = ConvertToViewModel(category);

            return View(categoryModel);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name,Id")] CategoryViewModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                var category = ConvertToModel(categoryModel);

                _context.Insert(category);
                _context.Save();
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Invalid category data.");
            return View(categoryModel);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(long id)
        {
            var category = _context.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            CategoryViewModel categoryModel = ConvertToViewModel(category);

            return View(categoryModel);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long id, [Bind("Name,Id")] CategoryViewModel categoryModel)
        {
            if (id != categoryModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var category = ConvertToModel(categoryModel);

                    _context.Update(category);
                    _context.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(categoryModel.Id))
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
            return View(categoryModel);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(long id)
        {
            var category = _context.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            CategoryViewModel categoryModel = ConvertToViewModel(category);

            return View(categoryModel);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            _context.Delete(id);
            _context.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(long id)
        {
            return _context.Exists(id);
        }

        private Category ConvertToModel(CategoryViewModel categoryModel)
        {
            Category category = new Category
            {
                Id = categoryModel.Id,
                Name = categoryModel.Name
            };

            return category;
        }

        private CategoryViewModel ConvertToViewModel(Category category)
        {
            var categoryModel = new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name
            };

            return categoryModel;
        }
    }
}
