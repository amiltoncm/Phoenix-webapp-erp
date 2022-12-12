using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Phoenix.Data;
using Phoenix.Domains;
using Phoenix.Models;

namespace Phoenix.Controllers
{
    public class PaymentTermsController : Controller
    {
        private readonly PhoenixContext _context;

        public PaymentTermsController(PhoenixContext context)
        {
            _context = context;
        }

        // GET: PaymentTerms
        public async Task<IActionResult> Index()
        {
            var phoenixContext = _context.PaymentTerm.Include(p => p.PaymentIndication).Include(p => p.Status);
            return View(await phoenixContext.ToListAsync());
        }

        // GET: PaymentTerms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PaymentTerm == null)
            {
                return NotFound();
            }

            var paymentTerm = await _context.PaymentTerm
                .Include(p => p.PaymentIndication)
                .Include(p => p.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentTerm == null)
            {
                return NotFound();
            }

            return View(paymentTerm);
        }

        // GET: PaymentTerms/Create
        public IActionResult Create()
        {
            ViewData["PaymentIndicationId"] = new SelectList(_context.Set<PaymentIndication>(), "Id", "Name");
            ViewData["StatusId"] = new SelectList(_context.Status.OrderBy(s => s.Name), "Id", "Name");
            return View();
        }

        // POST: PaymentTerms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Installments,Period,Fees,PaymentIndicationId,Created,Updated,StatusId")] PaymentTerm paymentTerm)
        {
            if (ModelState.IsValid)
            {

                paymentTerm.Created = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                paymentTerm.Updated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                _context.Add(paymentTerm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentIndicationId"] = new SelectList(_context.Set<PaymentIndication>(), "Id", "Name", paymentTerm.PaymentIndicationId);
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name", paymentTerm.StatusId);
            return View(paymentTerm);
        }

        // GET: PaymentTerms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PaymentTerm == null)
            {
                return NotFound();
            }

            var paymentTerm = await _context.PaymentTerm.FindAsync(id);
            if (paymentTerm == null)
            {
                return NotFound();
            }
            ViewData["PaymentIndicationId"] = new SelectList(_context.Set<PaymentIndication>(), "Id", "Name", paymentTerm.PaymentIndicationId);
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name", paymentTerm.StatusId);
            return View(paymentTerm);
        }

        // POST: PaymentTerms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Installments,Period,Fees,PaymentIndicationId,Created,Updated,StatusId")] PaymentTerm paymentTerm)
        {
            if (id != paymentTerm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    paymentTerm.Created = DateTime.SpecifyKind(paymentTerm.Created, DateTimeKind.Utc);
                    paymentTerm.Updated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                    _context.Update(paymentTerm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentTermExists(paymentTerm.Id))
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
            ViewData["PaymentIndicationId"] = new SelectList(_context.Set<PaymentIndication>(), "Id", "Name", paymentTerm.PaymentIndicationId);
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name", paymentTerm.StatusId);
            return View(paymentTerm);
        }

        // GET: PaymentTerms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PaymentTerm == null)
            {
                return NotFound();
            }

            var paymentTerm = await _context.PaymentTerm
                .Include(p => p.PaymentIndication)
                .Include(p => p.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentTerm == null)
            {
                return NotFound();
            }

            return View(paymentTerm);
        }

        // POST: PaymentTerms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PaymentTerm == null)
            {
                return Problem("Entity set 'PhoenixContext.PaymentTerm'  is null.");
            }
            var paymentTerm = await _context.PaymentTerm.FindAsync(id);
            if (paymentTerm != null)
            {
                _context.PaymentTerm.Remove(paymentTerm);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentTermExists(int id)
        {
          return _context.PaymentTerm.Any(e => e.Id == id);
        }
    }
}
