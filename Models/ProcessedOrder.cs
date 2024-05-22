using Part2.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace Part2.Models
{
    public class ProcessedOrder
    {
        [Key]
        public int OrderId { get; set; }
        public int? CraftId { get; set; }
        public string? ClientId { get; set; }
        public DateTime OrderDate { get; set; }

        //navigation properties
        public Craft? Craft { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
