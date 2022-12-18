using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Phoenix.Data;
using Phoenix.Domains;
using Phoenix.Models;

namespace Phoenix.Controllers
{
    public class ChartAccountsController : Controller
    {
        private readonly PhoenixContext _context;

        public ChartAccountsController(PhoenixContext context)
        {
            _context = context;
        }

        // GET: ChartAccounts
        public async Task<IActionResult> Index()
        {
            var phoenixContext = _context.ChartAccounts.Include(c => c.MovimentType).Include(c => c.Status);
            return View(await phoenixContext.ToListAsync());
        }

        // GET: ChartAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ChartAccounts == null)
            {
                return NotFound();
            }

            var chartAccounts = await _context.ChartAccounts
                .Include(c => c.MovimentType)
                .Include(c => c.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chartAccounts == null)
            {
                return NotFound();
            }

            return View(chartAccounts);
        }

        // GET: ChartAccounts/Create
        public IActionResult Create()
        {
            ViewData["MovimentTypeId"] = new SelectList(_context.Set<MovimentType>(), "Id", "Name");
            ViewData["StatusId"] = new SelectList(_context.Status.OrderBy(s => s.Name), "Id", "Name");
            return View();
        }

        // POST: ChartAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,MovimentTypeId,Created,Updated,StatusId")] ChartAccounts chartAccounts)
        {
            if (ModelState.IsValid)
            {
                chartAccounts.Created = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                chartAccounts.Updated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                _context.Add(chartAccounts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovimentTypeId"] = new SelectList(_context.Set<MovimentType>(), "Id", "Id", chartAccounts.MovimentTypeId);
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name", chartAccounts.StatusId);
            return View(chartAccounts);
        }

        // GET: ChartAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ChartAccounts == null)
            {
                return NotFound();
            }

            var chartAccounts = await _context.ChartAccounts.FindAsync(id);
            if (chartAccounts == null)
            {
                return NotFound();
            }
            ViewData["MovimentTypeId"] = new SelectList(_context.Set<MovimentType>(), "Id", "Name", chartAccounts.MovimentTypeId);
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name", chartAccounts.StatusId);
            return View(chartAccounts);
        }

        // POST: ChartAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,MovimentTypeId,Created,Updated,StatusId")] ChartAccounts chartAccounts)
        {
            if (id != chartAccounts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    chartAccounts.Created = DateTime.SpecifyKind(chartAccounts.Created, DateTimeKind.Utc);
                    chartAccounts.Updated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                    _context.Update(chartAccounts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChartAccountsExists(chartAccounts.Id))
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
            ViewData["MovimentTypeId"] = new SelectList(_context.Set<MovimentType>(), "Id", "Id", chartAccounts.MovimentTypeId);
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name", chartAccounts.StatusId);
            return View(chartAccounts);
        }

        // GET: ChartAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ChartAccounts == null)
            {
                return NotFound();
            }

            var chartAccounts = await _context.ChartAccounts
                .Include(c => c.MovimentType)
                .Include(c => c.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chartAccounts == null)
            {
                return NotFound();
            }

            return View(chartAccounts);
        }

        // POST: ChartAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ChartAccounts == null)
            {
                return Problem("Entity set 'PhoenixContext.ChartAccounts'  is null.");
            }
            var chartAccounts = await _context.ChartAccounts.FindAsync(id);
            if (chartAccounts != null)
            {
                _context.ChartAccounts.Remove(chartAccounts);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChartAccountsExists(int id)
        {
            return _context.ChartAccounts.Any(e => e.Id == id);
        }
    }
}
