using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Phoenix.Data;
using Phoenix.Models;

namespace Phoenix.Controllers
{
    public class CitiesController : Controller
    {
        private readonly PhoenixContext _context;

        public CitiesController(PhoenixContext context)
        {
            _context = context;
        }

        // GET: Cities
        public async Task<IActionResult> Index(string searchString)
        {
            var phoenixContext = _context.City.Include(c => c.State).Include(c => c.Status).OrderBy(c => c.Name);

            if (!String.IsNullOrEmpty(searchString))
            {
                phoenixContext = _context.City
                                         .Include(c => c.State)
                                         .Include(c => c.Status)
                                         .Where(c => c.Name.Contains(searchString))
                                         .OrderBy(c => c.Name);
            }
            return View(await phoenixContext.ToListAsync());
        }

        // GET: Cities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.City == null)
            {
                return NotFound();
            }

            var city = await _context.City
                .Include(c => c.State)
                .Include(c => c.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // GET: Cities/Create
        public IActionResult Create()
        {
            ViewData["StateId"] = new SelectList(_context.State.OrderBy(s => s.Name), "Id", "Name");
            ViewData["StatusId"] = new SelectList(_context.Status.OrderBy(s => s.Name), "Id", "Name");
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Name,StateId,StatusId,Created,Updated")] City city)
        {
            if (ModelState.IsValid)
            {
                city.Created = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                city.Updated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                _context.Add(city);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StateId"] = new SelectList(_context.State.OrderBy(s => s.Name), "Id", "Name", city.StateId);
            ViewData["StatusId"] = new SelectList(_context.Status.OrderBy(s => s.Name), "Id", "Name", city.StatusId);
            return View(city);
        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.City == null)
            {
                return NotFound();
            }

            var city = await _context.City.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }
            ViewData["StateId"] = new SelectList(_context.State.OrderBy(s => s.Name), "Id", "Name", city.StateId);
            ViewData["StatusId"] = new SelectList(_context.Status.OrderBy(s => s.Name), "Id", "Name", city.StatusId);
            return View(city);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Name,StateId,StatusId,Created,Updated")] City city)
        {
            if (id != city.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(city);
                    city.Created = DateTime.SpecifyKind(city.Created, DateTimeKind.Utc);
                    city.Updated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityExists(city.Id))
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
            ViewData["StateId"] = new SelectList(_context.State.OrderBy(s => s.Name), "Id", "Name", city.StateId);
            ViewData["StatusId"] = new SelectList(_context.Status.OrderBy(s => s.Name), "Id", "Name", city.StatusId);
            return View(city);
        }

        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.City == null)
            {
                return NotFound();
            }

            var city = await _context.City
                .Include(c => c.State)
                .Include(c => c.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.City == null)
            {
                return Problem("Entity set 'PhoenixContext.City'  is null.");
            }
            var city = await _context.City.FindAsync(id);
            if (city != null)
            {
                _context.City.Remove(city);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CityExists(int id)
        {
            return _context.City.Any(e => e.Id == id);
        }
    }
}
