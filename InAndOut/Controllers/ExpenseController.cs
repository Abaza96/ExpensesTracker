using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InAndOut.Data;
using InAndOut.Models;

namespace InAndOut.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ExpenseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Expenses
        public async Task<IActionResult> Index(string sortOrder, string searchFilter)
        {
            var Expense = string.IsNullOrEmpty(searchFilter) ? 
                _context.Expenses.Select(q => q) : 
                _context.Expenses.Where(q => q.Name.Contains(searchFilter) || 
                                        q.Cost.ToString().Contains(searchFilter));
            ViewBag.ESortOrder = sortOrder == "dExpense" ? "aExpense" : "dExpense";
            ViewBag.ASortOrder = sortOrder == "dAmount" ? "aAmount" : "dAmount";
            ViewBag.CSortOrder = sortOrder == "dCreated" ? "aCreated" : "dCreated";
            ViewBag.USortOrder = sortOrder == "dUpdated" ? "aUpdated" : "dUpdated";
            ViewBag.SearchFilter = searchFilter;

            Expense = sortOrder switch
            {
                "aExpense" => Expense.OrderBy(q => q.Name),
                "aAmount"  => Expense.OrderBy(q => q.Cost),
                "aCreated" => Expense.OrderBy(q => q.CreatedAt),
                "aUpdated" => Expense.OrderBy(q => q.UpdatedAt),
                "dExpense" => Expense.OrderByDescending(q => q.Name),
                "dAmount"  => Expense.OrderByDescending(q => q.Cost),
                "dCreated" => Expense.OrderByDescending(q => q.CreatedAt),
                "dUpdated" => Expense.OrderByDescending(q => q.UpdatedAt),
                _          => Expense.OrderBy(q => q.ExpenseID)
            };

            return View(await Expense.AsNoTracking().ToListAsync());
        }

        // GET: Expenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses
                .FirstOrDefaultAsync(m => m.ExpenseID == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // GET: Expenses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExpenseID,Name,Cost,CreatedAt,UpdatedAt")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expense);
        }

        // GET: Expenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            return View(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExpenseID,Name,Cost,CreatedAt,UpdatedAt")] Expense expense)
        {
            if (id != expense.ExpenseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseExists(expense.ExpenseID))
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
            return View(expense);
        }

        // GET: Expenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses
                .FirstOrDefaultAsync(m => m.ExpenseID == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expenses.Any(e => e.ExpenseID == id);
        }
    }
}
