using InAndOut.Data;
using InAndOut.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ItemController(ApplicationDbContext context)
        {
            _db = context;
        }
        public IActionResult Index(string sortOrder, string searchString)
        {
            var Item = string.IsNullOrEmpty(searchString) ? 
                _db.Items.Select(q => q) : 
                _db.Items.Where(q => q.Borrower.Contains(searchString) || 
                                     q.Lender.Contains(searchString)   || 
                                     q.ItemName.Contains(searchString));
            ViewBag.BSortOrder = string.IsNullOrEmpty(sortOrder) ? "aBorrower" : "dBorrower";
            ViewBag.LSortOrder = string.IsNullOrEmpty(sortOrder) ? "aLender"   : "dLender";
            ViewBag.ISortOrder = string.IsNullOrEmpty(sortOrder) ? "aItem"     : "dItem";
            ViewBag.CSortOrder = string.IsNullOrEmpty(sortOrder) ? "aCreated"  : "dCreated";
            ViewBag.USortOrder = string.IsNullOrEmpty(sortOrder) ? "aUpdated"  : "dUpdated";
            ViewBag.SearchFilter = searchString;

            Item = sortOrder switch
            {
                "aBorrower" => Item.OrderBy(q => q.Borrower),
                "dBorrower" => Item.OrderByDescending(q => q.Borrower),
                "aLender"   => Item.OrderBy(q => q.Lender),
                "dLender"   => Item.OrderByDescending(q => q.Lender),
                "aItem"     => Item.OrderBy(q => q.ItemName),
                "dItem"     => Item.OrderByDescending(q => q.ItemName),
                "aCreated"  => Item.OrderBy(q => q.CreatedAt),
                "dCreated"  => Item.OrderByDescending(q => q.CreatedAt),
                "aUpdated"  => Item.OrderBy(q => q.UpdatedAt),
                "dUpdated"  => Item.OrderByDescending(q => q.UpdatedAt),
                _           => Item.OrderBy(q => q.ItemID)
            };
            return View(Item.AsNoTracking().ToList());
        }
        public IActionResult Details(int? id) => id == null ? NotFound() : _db.Items.Find(id) == null ? NotFound() : View(_db.Items.Find(id));
        public IActionResult Edit(int? id) => id == null ? NotFound() : _db.Items.Find(id) == null ? NotFound() : View(_db.Items.Find(id));
        public IActionResult Delete(int? id) => id == null ? NotFound() : _db.Items.FirstOrDefault(q => q.ItemID == id) == null ? 
                                                        NotFound() : View(_db.Items.FirstOrDefault(q => q.ItemID == id));
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ItemID, Lender, Borrower, ItemName, CreatedAt, UpdatedAt")] Item item)
        {
            if (id != item.ItemID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(item);
                    _db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_db.Items.Any(q => q.ItemID == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return RedirectToAction(nameof(Index));

        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteExecuted(int id)
        {
            try
            {
                _db.Items.Remove(_db.Items.Find(id));
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete));
            }
        }
    }
}
