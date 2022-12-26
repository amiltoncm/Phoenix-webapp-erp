using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Phoenix.Data;
using Phoenix.Domains;
using Phoenix.Models;

namespace Phoenix.Controllers
{
    public class PersonAddressesController : Controller
    {
        private readonly PhoenixContext _context;

        public PersonAddressesController(PhoenixContext context)
        {
            _context = context;
        }

        // GET: PersonAddresses
        public async Task<IActionResult> Index(int ? personId)
        {
            var phoenixContext = _context.PersonAddress
                        .Include(p => p.AddressType)
                        .Include(p => p.City)
                        .Include(p => p.Person)
                        .Include(p => p.PublicPlace)
                        .Where(p => p.PersonId.Equals(personId))
                        .OrderBy(p => p.Address);
            return View(await phoenixContext.ToListAsync());
        }

        // GET: PersonAddresses by person
        public async Task<IActionResult> IndexPersonAdresses(int ? personId)
        {
            var phoenixContext = _context.PersonAddress
                                    .Include(p => p.AddressType)
                                    .Include(p => p.City)
                                    .Include(p => p.Person)
                                    .Include(p => p.PublicPlace)
                                    .Where(p => p.PersonId.Equals(personId))
                                    .OrderBy(p => p.Address);
            return View(await phoenixContext.ToListAsync());
        }

        // GET: PersonAddresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PersonAddress == null)
            {
                return NotFound();
            }

            var personAddress = await _context.PersonAddress
                .Include(p => p.AddressType)
                .Include(p => p.City)
                .Include(p => p.Person)
                .Include(p => p.PublicPlace)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personAddress == null)
            {
                return NotFound();
            }

            return View(personAddress);
        }

        // GET: PersonAddresses/Create
        public IActionResult Create()
        {
            ViewData["AddressTypeId"] = new SelectList(_context.Set<AddressType>(), "Id", "Name");
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name");
            ViewData["PersonId"] = new SelectList(_context.Person, "Id", "Alias");
            ViewData["PublicPlaceId"] = new SelectList(_context.Set<PublicPlace>(), "Id", "Name");
            return View();
        }

        // POST: PersonAddresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PersonId,AddressTypeId,PublicPlaceId,Address,Number,Complement,Reference,District,CityId,Zip")] PersonAddress personAddress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressTypeId"] = new SelectList(_context.Set<AddressType>(), "Id", "Name", personAddress.AddressTypeId);
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name", personAddress.CityId);
            ViewData["PersonId"] = new SelectList(_context.Person, "Id", "Alias", personAddress.PersonId);
            ViewData["PublicPlaceId"] = new SelectList(_context.Set<PublicPlace>(), "Id", "Name", personAddress.PublicPlaceId);
            return View(personAddress);
        }

        // GET: PersonAddresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PersonAddress == null)
            {
                return NotFound();
            }

            var personAddress = await _context.PersonAddress.FindAsync(id);
            if (personAddress == null)
            {
                return NotFound();
            }
            ViewData["AddressTypeId"] = new SelectList(_context.Set<AddressType>(), "Id", "Name", personAddress.AddressTypeId);
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name", personAddress.CityId);
            ViewData["PersonId"] = new SelectList(_context.Person, "Id", "Alias", personAddress.PersonId);
            ViewData["PublicPlaceId"] = new SelectList(_context.Set<PublicPlace>(), "Id", "Name", personAddress.PublicPlaceId);
            return View(personAddress);
        }

        // POST: PersonAddresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PersonId,AddressTypeId,PublicPlaceId,Address,Number,Complement,Reference,District,CityId,Zip")] PersonAddress personAddress)
        {
            if (id != personAddress.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonAddressExists(personAddress.Id))
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
            ViewData["AddressTypeId"] = new SelectList(_context.Set<AddressType>(), "Id", "Name", personAddress.AddressTypeId);
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name", personAddress.CityId);
            ViewData["PersonId"] = new SelectList(_context.Person, "Id", "Alias", personAddress.PersonId);
            ViewData["PublicPlaceId"] = new SelectList(_context.Set<PublicPlace>(), "Id", "Name", personAddress.PublicPlaceId);
            return View(personAddress);
        }

        // GET: PersonAddresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PersonAddress == null)
            {
                return NotFound();
            }

            var personAddress = await _context.PersonAddress
                .Include(p => p.AddressType)
                .Include(p => p.City)
                .Include(p => p.Person)
                .Include(p => p.PublicPlace)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personAddress == null)
            {
                return NotFound();
            }

            return View(personAddress);
        }

        // POST: PersonAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PersonAddress == null)
            {
                return Problem("Entity set 'PhoenixContext.PersonAddress'  is null.");
            }
            var personAddress = await _context.PersonAddress.FindAsync(id);
            if (personAddress != null)
            {
                _context.PersonAddress.Remove(personAddress);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonAddressExists(int id)
        {
          return _context.PersonAddress.Any(e => e.Id == id);
        }
    }
}
