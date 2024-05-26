using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Part2.Data;
using Part2.Models;

namespace Part2.Controllers
{
    public class ProcessedOrdersController : Controller
    {
        private readonly Part2Context _context;

        public ProcessedOrdersController(Part2Context context)
        {
            _context = context;
        }

        // GET: ProcessedOrders
        public async Task<IActionResult> Index()
        {
            var part2Context = _context.ProcessedOrders.Include(p => p.Craft);
            return View(await part2Context.ToListAsync());
        }

        // GET: ProcessedOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var processedOrder = await _context.ProcessedOrders
                .Include(p => p.Craft)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (processedOrder == null)
            {
                return NotFound();
            }

            return View(processedOrder);
        }

        // GET: ProcessedOrders/Create
        public IActionResult Create()
        {
            ViewData["CraftId"] = new SelectList(_context.Crafts, "Id", "Id");
            return View();
        }

        // POST: ProcessedOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,CraftId,ClientId,OrderDate")] ProcessedOrder processedOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(processedOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CraftId"] = new SelectList(_context.Crafts, "Id", "Id", processedOrder.CraftId);
            return View(processedOrder);
        }

        // GET: ProcessedOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var processedOrder = await _context.ProcessedOrders.FindAsync(id);
            if (processedOrder == null)
            {
                return NotFound();
            }
            ViewData["CraftId"] = new SelectList(_context.Crafts, "Id", "Id", processedOrder.CraftId);
            return View(processedOrder);
        }

        // POST: ProcessedOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,CraftId,ClientId,OrderDate")] ProcessedOrder processedOrder)
        {
            if (id != processedOrder.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(processedOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcessedOrderExists(processedOrder.OrderId))
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
            ViewData["CraftId"] = new SelectList(_context.Crafts, "Id", "Id", processedOrder.CraftId);
            return View(processedOrder);
        }

        // GET: ProcessedOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var processedOrder = await _context.ProcessedOrders
                .Include(p => p.Craft)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (processedOrder == null)
            {
                return NotFound();
            }

            return View(processedOrder);
        }

        // POST: ProcessedOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var processedOrder = await _context.ProcessedOrders.FindAsync(id);
            if (processedOrder != null)
            {
                _context.ProcessedOrders.Remove(processedOrder);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcessedOrderExists(int id)
        {
            return _context.ProcessedOrders.Any(e => e.OrderId == id);
        }
    }
}
