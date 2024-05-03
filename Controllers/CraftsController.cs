using Microsoft.AspNetCore.Mvc;
using Part2.Models;

namespace Part2.Controllers
{
    public class CraftsController : Controller
    {

        // GET: Crafts
        public IActionResult Index()
        {
            //Simulate fetching products from a database
            var crafts = new List<Craft>
            {
                new Craft { Id = 1, CraftName = "Weaved Baskets", CraftDescription = "A traditional South African crafts weaved basket, intricately handwoven with vibrant colors and patterns, embodying the rich cultural heritage and skilled artistry of its makers.", imgUrl = "/images/weavedBasket.jpeg", Price = 350 },
                new Craft { Id = 2, CraftName = "Beadwork", CraftDescription = "South African beadwork crafts are vibrant expressions of cultural heritage, blending intricate bead weaving techniques with symbolic patterns and motifs.", imgUrl = "/images/Beadwork.jpg", Price = 100},
                new Craft { Id = 3, CraftName = "Pottery", CraftDescription = "South African pottery is a fusion of rich cultural traditions, blending indigenous African techniques with influences from Europe and Asia. Crafted with locally sourced clay, these pieces feature intricate designs inspired by nature, folklore, and cultural symbolism.", imgUrl = "/images/pottery.jpg", Price = 200 },
                new Craft { Id = 4, CraftName = "Wooden Crafts", CraftDescription="South African woodwork craft combines intricate carving techniques with vibrant local woods, often featuring traditional motifs. It reflects the nation's rich cultural heritage through functional items like furniture and decorative sculptures.", imgUrl="/images/woodwork.jpg", Price=150 },
            };

            //Return the Craft folder's Index View passing in the crafts to be displayed as an argument
            return View(crafts);
        }
    }
}


// CODE ATTRIBUTION
//Anderson, R (2024) Get started with ASP.NET Core MVC [SourceCode]. https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/start-mvc?view=aspnetcore-8.0&tabs=visual-studio


