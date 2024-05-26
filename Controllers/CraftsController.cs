using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Part2.Areas.Identity.Data;
using Part2.Data;
using Part2.Models;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Part2.Controllers
{
    [Authorize]
    public class CraftsController : Controller
    {
        private readonly Part2Context _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CraftsController(Part2Context context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Crafts
        public async Task<IActionResult> Index()
        {
            var availableCrafts = await _context.Crafts.Where(c => c.IsAvailable).ToListAsync();
            return View(availableCrafts);
        }

        // GET: Crafts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Crafts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CraftName,imgUrl,CraftDescription,Price")] Craft craft, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    craft.imgUrl = await SaveImage(imageFile);
                }

                _context.Add(craft);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(craft);
        }

        // GET: Crafts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var craft = await _context.Crafts.FindAsync(id);
            if (craft == null)
            {
                return NotFound();
            }
            return View(craft);
        }

        // POST: Crafts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CraftName,CraftDescription,Price,imgUrl")] Craft craft, IFormFile imageFile)
        {
            if (id != craft.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingCraft = await _context.Crafts.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
                    if (existingCraft == null)
                    {
                        return NotFound();
                    }

                    // Check if a new image file was uploaded
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        craft.imgUrl = await SaveImage(imageFile);
                    }
                    else
                    {
                        // Retain the existing image URL if no new image was uploaded
                        craft.imgUrl = existingCraft.imgUrl;
                    }

                    // Update the craft entity
                    _context.Entry(craft).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CraftExists(craft.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(craft);
        }

        // GET: Crafts/EditImage/5
        public async Task<IActionResult> EditImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var craft = await _context.Crafts.FindAsync(id);
            if (craft == null)
            {
                return NotFound();
            }
            return View(craft);
        }

        // POST: Crafts/EditImage/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditImage(int id, IFormFile imageFile)
        {
            var craft = await _context.Crafts.FindAsync(id);
            if (craft == null)
            {
                return NotFound();
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                craft.imgUrl = await SaveImage(imageFile);
                _context.Update(craft);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(craft);
        }

        private async Task<string> SaveImage(IFormFile imageFile)
        {
            var fileName = Path.GetFileName(imageFile.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images")))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images"));
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return "/images/" + fileName;
        }

        private bool CraftExists(int id)
        {
            return _context.Crafts.Any(e => e.Id == id);
        }

        public async Task<IActionResult> ConfirmOrder(int id)
        {
            var craft = await _context.Crafts.FindAsync(id);
            if (craft == null)
            {
                return NotFound();
            }
            return View(craft);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(int id)
        {
            var craft = await _context.Crafts.FindAsync(id);
            if (craft == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            Order order = new Order
            {
                CraftId = craft.Id,
                ClientId = userId
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            OrderHistory orderHistory = new OrderHistory
            {
                OrderId = order.OrderId,
                ClientId = userId,
                OrderDate = DateTime.Now,
                Order = order,
                User = await _userManager.FindByIdAsync(userId)
            };

            _context.OrderHistories.Add(orderHistory);
            craft.IsAvailable = false;
            _context.Crafts.Update(craft);

            await _context.SaveChangesAsync();

            return RedirectToAction("OrderSuccess");
        }

        public IActionResult OrderSuccess()
        {
            return View();
        }
    }
}
