using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Part2.Models
{
    //The Model
    //Outlines the attrbutes of the Craft class and their respective datatypes. 
    public class Craft
    {
        public int Id { get; set; }
        [Display(Name = "Craft Name")]
        public string? CraftName { get; set; }
        [Display(Name = "Image")]
        public string? imgUrl { get; set; }
        [Display(Name = "Craft Description")]
        public string? CraftDescription { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}
