﻿using System;
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
    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Transactions.Include(t => t.Checkout).Include(t => t.Payment).Include(t => t.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Checkout)
                .Include(t => t.Payment)
                .Include(t => t.Product)
                .FirstOrDefaultAsync(m => m.Transaction_ID == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            ViewData["Checkout_ID"] = new SelectList(_context.Checkouts, "Checkout_ID", "Checkout_ID");
            ViewData["Payment_ID"] = new SelectList(_context.Payments, "Payment_ID", "Payment_ID");
            ViewData["Product_ID"] = new SelectList(_context.Products, "ProductID", "Product_description");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Transaction_ID,Product_ID,Payment_ID,Checkout_ID,Status,TransactionDate")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Checkout_ID"] = new SelectList(_context.Checkouts, "Checkout_ID", "Checkout_ID", transaction.Checkout_ID);
            ViewData["Payment_ID"] = new SelectList(_context.Payments, "Payment_ID", "Payment_ID", transaction.Payment_ID);
            ViewData["Product_ID"] = new SelectList(_context.Products, "ProductID", "Product_description", transaction.Product_ID);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["Checkout_ID"] = new SelectList(_context.Checkouts, "Checkout_ID", "Checkout_ID", transaction.Checkout_ID);
            ViewData["Payment_ID"] = new SelectList(_context.Payments, "Payment_ID", "Payment_ID", transaction.Payment_ID);
            ViewData["Product_ID"] = new SelectList(_context.Products, "ProductID", "Product_description", transaction.Product_ID);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Transaction_ID,Product_ID,Payment_ID,Checkout_ID,Status,TransactionDate")] Transaction transaction)
        {
            if (id != transaction.Transaction_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.Transaction_ID))
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
            ViewData["Checkout_ID"] = new SelectList(_context.Checkouts, "Checkout_ID", "Checkout_ID", transaction.Checkout_ID);
            ViewData["Payment_ID"] = new SelectList(_context.Payments, "Payment_ID", "Payment_ID", transaction.Payment_ID);
            ViewData["Product_ID"] = new SelectList(_context.Products, "ProductID", "Product_description", transaction.Product_ID);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Checkout)
                .Include(t => t.Payment)
                .Include(t => t.Product)
                .FirstOrDefaultAsync(m => m.Transaction_ID == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transactions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Transactions'  is null.");
            }
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
          return (_context.Transactions?.Any(e => e.Transaction_ID == id)).GetValueOrDefault();
        }
    }
}
