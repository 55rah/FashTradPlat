using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FashTradPlat.Areas.Identity.Data;
using FashTradPlat.Models;

namespace FashTradPlat.Controllers
{
    public class CheckoutsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CheckoutsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Checkouts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Checkouts.Include(c => c.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Checkouts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Checkouts == null)
            {
                return NotFound();
            }

            var checkout = await _context.Checkouts
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Checkout_ID == id);
            if (checkout == null)
            {
                return NotFound();
            }

            return View(checkout);
        }

        // GET: Checkouts/Create
        public IActionResult Create()
        {
            ViewData["Product_ID"] = new SelectList(_context.Products, "ProductID", "ProductID");
            return View();
        }

        // POST: Checkouts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Checkout_ID,Product_ID,Price")] Checkout checkout)
        {
            if (ModelState.IsValid)
            {
                _context.Add(checkout);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Product_ID"] = new SelectList(_context.Products, "ProductID", "ProductID", checkout.Product_ID);
            return View(checkout);
        }

        // GET: Checkouts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Checkouts == null)
            {
                return NotFound();
            }

            var checkout = await _context.Checkouts.FindAsync(id);
            if (checkout == null)
            {
                return NotFound();
            }
            ViewData["Product_ID"] = new SelectList(_context.Products, "ProductID", "ProductID", checkout.Product_ID);
            return View(checkout);
        }

        // POST: Checkouts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Checkout_ID,Product_ID,Price")] Checkout checkout)
        {
            if (id != checkout.Checkout_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checkout);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CheckoutExists(checkout.Checkout_ID))
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
            ViewData["Product_ID"] = new SelectList(_context.Products, "ProductID", "ProductID", checkout.Product_ID);
            return View(checkout);
        }

        // GET: Checkouts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Checkouts == null)
            {
                return NotFound();
            }

            var checkout = await _context.Checkouts
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Checkout_ID == id);
            if (checkout == null)
            {
                return NotFound();
            }

            return View(checkout);
        }

        // POST: Checkouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Checkouts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Checkouts'  is null.");
            }
            var checkout = await _context.Checkouts.FindAsync(id);
            if (checkout != null)
            {
                _context.Checkouts.Remove(checkout);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CheckoutExists(int id)
        {
          return (_context.Checkouts?.Any(e => e.Checkout_ID == id)).GetValueOrDefault();
        }
    }
}
