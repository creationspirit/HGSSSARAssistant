using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using HGSSSARAssistant.BLL;
using HGSSSARAssistant.BLL.BusinessEntities;

namespace HGSSSARAssistant.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserBLL _context;

        public UsersController(UserBLL context)
        {
            _context = context;
        }

        // GET: Users
        public ActionResult Index()
        {
            return View(_context.GetAllUsers());
        }

        // GET: Users/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _context.GetUserById((long) id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("FirstName,LastName,Address,Email,AndroidPushId,Password,PasswordSalt,ContactNumber,AdditionalContactNumbers,Id")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.AddUser(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _context.GetUserById((long)id);
            user = _context.UpdateUser(user);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long id, [Bind("FirstName,LastName,Address,Email,AndroidPushId,Password,PasswordSalt,ContactNumber,AdditionalContactNumbers,Id")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    user = _context.UpdateUser(user);   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _context.GetUserById((long) id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            _context.DeleteUser(id);
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(long id)
        {
            var user = _context.GetUserById((long)id);

            return user != null;
        }
    }
}
