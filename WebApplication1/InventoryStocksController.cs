using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1
{
    public class InventoryStocksController : Controller
    {
        private readonly WebApplication1Context _context;

        public InventoryStocksController(WebApplication1Context context)
        {
            _context = context;
        }

        // GET: InventoryStocks
        public async Task<IActionResult> Index()
        {
            var webApplication1Context = _context.InventoryStocks.Include(i => i.Product);
            return View(await webApplication1Context.ToListAsync());
        }

        // GET: InventoryStocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryStock = await _context.InventoryStocks
                .Include(i => i.Product)
                .FirstOrDefaultAsync(m => m.BatchId == id);
            if (inventoryStock == null)
            {
                return NotFound();
            }

            return View(inventoryStock);
        }

        // GET: InventoryStocks/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Company");
            return View();
        }

        // POST: InventoryStocks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BatchId,ProductId,Quantity,PurchaseDateAd,PurchaseDateBs,CostPrice")] InventoryStock inventoryStock)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventoryStock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Company", inventoryStock.ProductId);
            return View(inventoryStock);
        }

        // GET: InventoryStocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryStock = await _context.InventoryStocks.FindAsync(id);
            if (inventoryStock == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Company", inventoryStock.ProductId);
            return View(inventoryStock);
        }

        // POST: InventoryStocks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BatchId,ProductId,Quantity,PurchaseDateAd,PurchaseDateBs,CostPrice")] InventoryStock inventoryStock)
        {
            if (id != inventoryStock.BatchId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventoryStock);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryStockExists(inventoryStock.BatchId))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Company", inventoryStock.ProductId);
            return View(inventoryStock);
        }

        // GET: InventoryStocks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryStock = await _context.InventoryStocks
                .Include(i => i.Product)
                .FirstOrDefaultAsync(m => m.BatchId == id);
            if (inventoryStock == null)
            {
                return NotFound();
            }

            return View(inventoryStock);
        }

        // POST: InventoryStocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventoryStock = await _context.InventoryStocks.FindAsync(id);
            if (inventoryStock != null)
            {
                _context.InventoryStocks.Remove(inventoryStock);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryStockExists(int id)
        {
            return _context.InventoryStocks.Any(e => e.BatchId == id);
        }
    }
}
