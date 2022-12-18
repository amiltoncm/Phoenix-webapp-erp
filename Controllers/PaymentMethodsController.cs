using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Phoenix.Data;
using Phoenix.Domains;
using Phoenix.Models;

namespace Phoenix.Controllers
{
    public class PaymentMethodsController : Controller
    {
        private readonly PhoenixContext _context;

        public PaymentMethodsController(PhoenixContext context)
        {
            _context = context;
        }

        // GET: PaymentMethods
        public async Task<IActionResult> Index()
        {
            var phoenixContext = _context.PaymentMethod.Include(p => p.Account).Include(p => p.PaymentType).Include(p => p.Status);
            return View(await phoenixContext.ToListAsync());
        }

        // GET: PaymentMethods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PaymentMethod == null)
            {
                return NotFound();
            }

            var paymentMethod = await _context.PaymentMethod
                .Include(p => p.Account)
                .Include(p => p.PaymentType)
                .Include(p => p.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentMethod == null)
            {
                return NotFound();
            }

            return View(paymentMethod);
        }

        // GET: PaymentMethods/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Account.OrderBy(a => a.Name), "Id", "Name");
            ViewData["PaymentTypeId"] = new SelectList(_context.Set<PaymentType>(), "Id", "Name");
            ViewData["StatusId"] = new SelectList(_context.Status.OrderBy(s => s.Name), "Id", "Name");
            return View();
        }

        // POST: PaymentMethods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PaymentTypeId,AccountId,Days,StatusId,Created,Updated")] PaymentMethod paymentMethod)
        {
            if (ModelState.IsValid)
            {
                paymentMethod.Created = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                paymentMethod.Updated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                _context.Add(paymentMethod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Account.OrderBy(a => a.Name), "Id", "Name", paymentMethod.AccountId);
            ViewData["PaymentTypeId"] = new SelectList(_context.Set<PaymentType>().OrderBy(p => p.Name), "Id", "Name", paymentMethod.PaymentTypeId);
            ViewData["StatusId"] = new SelectList(_context.Status.OrderBy(s => s.Name), "Id", "Name", paymentMethod.StatusId);
            return View(paymentMethod);
        }

        // GET: PaymentMethods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PaymentMethod == null)
            {
                return NotFound();
            }

            var paymentMethod = await _context.PaymentMethod.FindAsync(id);
            if (paymentMethod == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Account.OrderBy(a => a.Name), "Id", "Name", paymentMethod.AccountId);
            ViewData["PaymentTypeId"] = new SelectList(_context.Set<PaymentType>().OrderBy(p => p.Name), "Id", "Name", paymentMethod.PaymentTypeId);
            ViewData["StatusId"] = new SelectList(_context.Status.OrderBy(s => s.Name), "Id", "Name", paymentMethod.StatusId);
            return View(paymentMethod);
        }

        // POST: PaymentMethods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PaymentTypeId,AccountId,Days,StatusId,Created,Updated")] PaymentMethod paymentMethod)
        {
            if (id != paymentMethod.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    paymentMethod.Created = DateTime.SpecifyKind(paymentMethod.Created, DateTimeKind.Utc);
                    paymentMethod.Updated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                    _context.Update(paymentMethod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentMethodExists(paymentMethod.Id))
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
            ViewData["AccountId"] = new SelectList(_context.Account.OrderBy(a => a.Name), "Id", "Name", paymentMethod.AccountId);
            ViewData["PaymentTypeId"] = new SelectList(_context.Set<PaymentType>().OrderBy(p => p.Name), "Id", "Name", paymentMethod.PaymentTypeId);
            ViewData["StatusId"] = new SelectList(_context.Status.OrderBy(s => s.Name), "Id", "Name", paymentMethod.StatusId);
            return View(paymentMethod);
        }

        // GET: PaymentMethods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PaymentMethod == null)
            {
                return NotFound();
            }

            var paymentMethod = await _context.PaymentMethod
                .Include(p => p.Account)
                .Include(p => p.PaymentType)
                .Include(p => p.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentMethod == null)
            {
                return NotFound();
            }

            return View(paymentMethod);
        }

        // POST: PaymentMethods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PaymentMethod == null)
            {
                return Problem("Entity set 'PhoenixContext.PaymentMethod'  is null.");
            }
            var paymentMethod = await _context.PaymentMethod.FindAsync(id);
            if (paymentMethod != null)
            {
                _context.PaymentMethod.Remove(paymentMethod);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentMethodExists(int id)
        {
            return _context.PaymentMethod.Any(e => e.Id == id);
        }
    }
}
