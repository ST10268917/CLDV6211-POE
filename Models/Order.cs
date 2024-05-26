using System.ComponentModel.DataAnnotations;
using Part2.Areas.Identity.Data;

namespace Part2.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int? CraftId { get; set; }
        public string? ClientId { get; set; }
        public DateTime OrderDate { get; set; }

        public bool IsNotProcessed { get; set; } = true;

        //navigation properties
        public Craft? Craft { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
