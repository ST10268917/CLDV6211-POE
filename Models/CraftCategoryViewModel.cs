using Microsoft.AspNetCore.Mvc.Rendering;

namespace Part2.Models
{
    public class CraftCategoryViewModel
    {
        public List<Craft>? Crafts { get; set; }
        public SelectList? Categories { get; set; }
        public string? CraftCategory { get; set; }
        public string? SearchString { get; set; }
    }
}
