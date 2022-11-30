using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Phoenix.Data;
using Phoenix.Domains;
using Phoenix.Models;

namespace Phoenix.Controllers
{
    public class PeopleController : Controller
    {
        private readonly PhoenixContext _context;

        public PeopleController(PhoenixContext context)
        {
            _context = context;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            var phoenixContext = _context.Person.Include(p => p.City)
                                                .Include(p => p.PersonType)
                                                .Include(p => p.PublicPlace)
                                                .Include(p => p.Status);
            return View(await phoenixContext.ToListAsync());
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                                       .Include(p => p.City)
                                       .Include(p => p.PersonType)
                                       .Include(p => p.PublicPlace)
                                       .Include(p => p.Status)
                                       .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.City.OrderBy(c => c.Name), "Id", "Name");
            ViewData["PersonTypeId"] = new SelectList(_context.Set<PersonType>(), "Id", "Name");
            ViewData["PublicPlaceId"] = new SelectList(_context.Set<PublicPlace>().OrderBy(p => p.Name), "Id", "Name");
            ViewData["StatusID"] = new SelectList(_context.Status.OrderBy(s => s.Name), "Id", "Name");
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Alias,Document,Registration,PublicPlaceId,Address,Number,Complement,District,CityId,Reference,Phone,Zip,PersonTypeId,Email,Client,Provider,Shipping,Associate,Created,Updated,Deleted,StatusID")] Person person)
        {
            if (person.Client.Equals(true))
            {
                person.Client = 1;
            }
            else
            {
                person.Client = 0;
            }
            if (person.Provider.Equals(true))
            {
                person.Provider = 1;
            }
            else
            {
                person.Provider = 0;
            }
            if (person.Shipping.Equals(true))
            {
                person.Shipping = 1;
            }
            else
            {
                person.Shipping = 0;
            }
            if (person.Associate.Equals(true))
            {
                person.Associate = 1;
            }
            else
            {
                person.Associate = 0;
            }

            if (ModelState.IsValid)
            {
                person.Created = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                person.Updated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name", person.CityId);
            ViewData["PersonTypeId"] = new SelectList(_context.Set<PersonType>(), "Id", "Id", person.PersonTypeId);
            ViewData["PublicPlaceId"] = new SelectList(_context.Set<PublicPlace>(), "Id", "Name", person.PublicPlaceId);
            ViewData["StatusID"] = new SelectList(_context.Status, "Id", "Name", person.StatusID);
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name", person.CityId);
            ViewData["PersonTypeId"] = new SelectList(_context.Set<PersonType>(), "Id", "Id", person.PersonTypeId);
            ViewData["PublicPlaceId"] = new SelectList(_context.Set<PublicPlace>(), "Id", "Name", person.PublicPlaceId);
            ViewData["StatusID"] = new SelectList(_context.Status, "Id", "Name", person.StatusID);
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Alias,Document,Registration,PublicPlaceId,Address,Number,Complement,District,CityId,Reference,Phone,Zip,PersonTypeId,Email,Client,Provider,Shipping,Associate,Created,Updated,Deleted,StatusID")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
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
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name", person.CityId);
            ViewData["PersonTypeId"] = new SelectList(_context.Set<PersonType>(), "Id", "Id", person.PersonTypeId);
            ViewData["PublicPlaceId"] = new SelectList(_context.Set<PublicPlace>(), "Id", "Name", person.PublicPlaceId);
            ViewData["StatusID"] = new SelectList(_context.Status, "Id", "Name", person.StatusID);
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .Include(p => p.City)
                .Include(p => p.PersonType)
                .Include(p => p.PublicPlace)
                .Include(p => p.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Person == null)
            {
                return Problem("Entity set 'PhoenixContext.Person'  is null.");
            }
            var person = await _context.Person.FindAsync(id);
            if (person != null)
            {
                _context.Person.Remove(person);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
          return _context.Person.Any(e => e.Id == id);
        }
    }
}
