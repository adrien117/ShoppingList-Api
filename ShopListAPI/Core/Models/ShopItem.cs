using System.ComponentModel.DataAnnotations;

namespace ShopListAPI.Core.Models
{
    public class ShopItem : BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; } // this guy here.... i need to be careful about it...
        [Required]
        public int Count { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        public int Rating { get; set; }
        [Required]
        public bool CheckedOut { get; set; }     
    }
}