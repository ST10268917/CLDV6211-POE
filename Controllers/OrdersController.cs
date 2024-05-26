using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Part2.Areas.Identity.Data;
using Part2.Data;
using Part2.Models;

namespace Part2.Controllers
{
    public class OrdersController : Controller
    {
        private readonly Part2Context _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(Part2Context context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }



        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var unprocessedOrders = await _context.Orders.Where(c => c.IsNotProcessed).ToListAsync();
            return View(unprocessedOrders);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Craft)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CraftId"] = new SelectList(_context.Crafts, "Id", "Id");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,CraftId,ClientId,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CraftId"] = new SelectList(_context.Crafts, "Id", "Id", order.CraftId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CraftId"] = new SelectList(_context.Crafts, "Id", "Id", order.CraftId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,CraftId,ClientId,OrderDate")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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
            ViewData["CraftId"] = new SelectList(_context.Crafts, "Id", "Id", order.CraftId);
            return View(order);
        }


        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }

        public async Task<IActionResult> ProcessOrder(int id)
        {
            var craft = await _context.Orders.FindAsync(id);
            if (craft == null)
            {
                return NotFound();
            }
            return View(craft);
        }

        public async Task<IActionResult> OrderProcessed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }


            ProcessedOrder processedOrder = new ProcessedOrder
            {
                CraftId = order.CraftId,
                ClientId = order.ClientId,
                OrderDate = order.OrderDate,
            
            };

            _context.ProcessedOrders.Add(processedOrder);
            order.IsNotProcessed = false;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return RedirectToAction("ProcessSuccess");
        }

        public IActionResult ProcessSuccess()
        {
            return View();
        }

        public async Task<IActionResult> OrderHistory()
        {
            var userId = _userManager.GetUserId(User);
            var orderHistories = await _context.OrderHistories
                .Include(oh => oh.Order)
                .Include(oh => oh.Order.Craft)
                .Where(oh => oh.ClientId == userId)
                .ToListAsync();

            return View(orderHistories);
        }

    }
}
