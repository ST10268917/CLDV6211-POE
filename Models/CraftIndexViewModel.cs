namespace Part2.Models
{
    public class CraftIndexViewModel
    {
        public IEnumerable<Craft> Crafts { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public string SelectedCategory { get; set; }
        public string CurrentFilter { get; set; }
    }
}
