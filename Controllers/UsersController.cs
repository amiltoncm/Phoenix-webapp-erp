using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Phoenix.Data;
using Phoenix.Models;

namespace Phoenix.Controllers
{
    public class UsersController : Controller
    {
        private readonly PhoenixContext _context;

        public UsersController(PhoenixContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index(string searchString)
        {

            var phoenixContext = _context.User.Include(u => u.Profile).Include(u => u.Status).OrderBy(u => u.Name);

            if (!String.IsNullOrEmpty(searchString))
            {
                phoenixContext = _context.User
                                         .Include(u => u.Profile)
                                         .Include(u => u.Status)
                                         .Where(u => u.Name.Contains(searchString))
                                         .OrderBy(u => u.Name);
            }

            return View(await phoenixContext.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .Include(u => u.Profile)
                .Include(u => u.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["ProfileId"] = new SelectList(_context.Set<Profile>().OrderBy(p => p.Name), "Id", "Name");
            ViewData["StatusId"] = new SelectList(_context.Status.OrderBy(s => s.Name), "Id", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Password,ProfileId,StatusId,Created,Updated,Deleted")] User user)
        {
            if (ModelState.IsValid)
            {
                if (user.StatusId == 0)
                {
                    user.Deleted = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                }
                user.Created = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                user.Updated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProfileId"] = new SelectList(_context.Set<Profile>().OrderBy(p => p.Name), "Id", "Name", user.ProfileId);
            ViewData["StatusId"] = new SelectList(_context.Status.OrderBy(s => s.Name), "Id", "Name", user.StatusId);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["ProfileId"] = new SelectList(_context.Set<Profile>().OrderBy(p => p.Name), "Id", "Name", user.ProfileId);
            ViewData["StatusId"] = new SelectList(_context.Status.OrderBy(s => s.Name), "Id", "Name", user.StatusId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Password,ConfirmPassword,ProfileId,StatusId,Created,Updated,Deleted")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (user.StatusId == 0)
                    {
                        user.Deleted = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                    } else
                    {
                        user.Deleted = null;
                    }
                    user.Created = DateTime.SpecifyKind(user.Created, DateTimeKind.Utc);
                    user.Updated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                    _context.Update(user);
                    await _context.SaveChangesAsync();
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
            ViewData["ProfileId"] = new SelectList(_context.Set<Profile>().OrderBy(p => p.Name), "Id", "Name", user.ProfileId);
            ViewData["StatusId"] = new SelectList(_context.Status.OrderBy(s => s.Name), "Id", "Name", user.StatusId);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .Include(u => u.Profile)
                .Include(u => u.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.User == null)
            {
                return Problem("Entity set 'PhoenixContext.User'  is null.");
            }
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                // _context.User.Remove(user);
                user.Deleted = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                user.StatusId = 0; 
                _context.Update(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return _context.User.Any(e => e.Id == id);
        }
    }
}
