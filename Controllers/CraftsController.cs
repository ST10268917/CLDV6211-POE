using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Part2.Areas.Identity.Data;
using Part2.Data;
using Part2.Models;

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
            // Fetch the list of crafts asynchronously from the database
            var crafts = await _context.Crafts.ToListAsync();
            return View(crafts);  // Pass the list of crafts to the view
        }

        // GET: Crafts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Crafts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CraftName,imgUrl,CraftDescription,Price")] Craft craft, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var fileName = Path.GetFileName(imageFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    craft.imgUrl = "/images/" + fileName; // Save relative path as URL
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
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
                    // Retrieve the existing craft to get the current imgUrl
                    var existingCraft = await _context.Crafts.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
                    if (existingCraft == null)
                    {
                        return NotFound();
                    }

                    // Check if a new image file was uploaded
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var fileName = Path.GetFileName(imageFile.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                        // Ensure the directory exists
                        if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images")))
                        {
                            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images"));
                        }

                        // Save the uploaded file to the server
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }
                        craft.imgUrl = "/images/" + fileName;
                    }
                    else
                    {
                        // Retain the existing image URL if no new image was uploaded
                        craft.imgUrl = existingCraft.imgUrl;
                    }

                    _context.Update(craft);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // If ModelState is invalid, retrieve the current image URL and return the view
                var existingCraft = await _context.Crafts.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
                craft.imgUrl = existingCraft?.imgUrl;
            }
            return View(craft);
        }

        private bool CraftExists(int id)
        {
            return _context.Crafts.Any(e => e.Id == id);
        }


        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.




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

            // Retrieve the current user's ID using UserManager
            var userId = _userManager.GetUserId(User);

            Order order = new Order
            {
                CraftId = craft.Id,
                ClientId = userId  // Use the retrieved user ID
            };

            _context.Orders.Add(order);
            //_context.Crafts.Remove(craft);
            await _context.SaveChangesAsync();

            return RedirectToAction("OrderSuccess");
        }

        public IActionResult OrderSuccess()
        {
            return View();
        }


    }
}


// CODE ATTRIBUTION
//Anderson, R (2024) Get started with ASP.NET Core MVC [SourceCode]. https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/start-mvc?view=aspnetcore-8.0&tabs=visual-studio


