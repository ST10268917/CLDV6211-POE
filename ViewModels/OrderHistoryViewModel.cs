using Part2.Models;

namespace Part2.ViewModels
{
    public class OrderHistoryViewModel
    {
        public List<Order>? Orders { get; set; }
        public string? FilterCraftName { get; set; }

    }
}
