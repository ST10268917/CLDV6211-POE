using Part2.Areas.Identity.Data;

namespace Part2.Models
{
    public class OrderHistory
    {
        public int OrderHistoryId { get; set; }
        public int? OrderId { get; set; }
        public string? ClientId { get; set; }
        public DateTime OrderDate { get; set; }
        public Order? Order { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
