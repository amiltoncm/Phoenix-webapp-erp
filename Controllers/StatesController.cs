using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Phoenix.Data;
using Phoenix.Models;

namespace Phoenix.Controllers
{
    public class StatesController : Controller
    {
        private readonly PhoenixContext _context;

        public StatesController(PhoenixContext context)
        {
            _context = context;
        }

        // GET: States
        public async Task<IActionResult> Index(string searchString)
        {
            var phoenixContext = _context.State.Include(s => s.Country).Include(s => s.Status).OrderBy(c => c.Name);

            if (!String.IsNullOrEmpty(searchString))
            {
                phoenixContext = _context.State
                                         .Include(s => s.Country)
                                         .Include(c => c.Status)
                                         .Where(c => c.Name.Contains(searchString))
                                         .OrderBy(c => c.Name);
            }

            return View(await phoenixContext.ToListAsync());
        }

        // GET: States/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.State == null)
            {
                return NotFound();
            }

            var state = await _context.State
                .Include(s => s.Country)
                .Include(s => s.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (state == null)
            {
                return NotFound();
            }

            return View(state);
        }

        // GET: States/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name");
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name");
            return View();
        }

        // POST: States/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Name,Abbreviation,CountryId,StatusId,Created,Updated")] State state)
        {
            if (ModelState.IsValid)
            {
                state.Abbreviation = state.Abbreviation.ToUpper();
                state.Created = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                state.Updated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                _context.Add(state);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name", state.CountryId);
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name", state.StatusId);
            return View(state);
        }

        // GET: States/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.State == null)
            {
                return NotFound();
            }

            var state = await _context.State.FindAsync(id);
            if (state == null)
            {
                return NotFound();
            }

            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name", state.CountryId);
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name", state.StatusId);
            return View(state);
        }

        // POST: States/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Name,Abbreviation,CountryId,StatusId,Created,Updated")] State state)
        {
            if (id != state.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    state.Abbreviation = state.Abbreviation.ToUpper();
                    state.Created = DateTime.SpecifyKind(state.Created, DateTimeKind.Utc);
                    state.Updated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                    _context.Update(state);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StateExists(state.Id))
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
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name", state.CountryId);
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name", state.StatusId);
            return View(state);
        }

        // GET: States/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.State == null)
            {
                return NotFound();
            }

            var state = await _context.State
                .Include(s => s.Country)
                .Include(s => s.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (state == null)
            {
                return NotFound();
            }

            return View(state);
        }

        // POST: States/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.State == null)
            {
                return Problem("Entity set 'PhoenixContext.State'  is null.");
            }
            var state = await _context.State.FindAsync(id);
            if (state != null)
            {
                _context.State.Remove(state);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StateExists(int id)
        {
          return _context.State.Any(e => e.Id == id);
        }
    }
}
