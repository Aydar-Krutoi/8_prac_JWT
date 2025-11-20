using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _8_prac_JWT.Models
{
    public class BasketItem
    {
        [Key]
        public int Basket_items_id { get; set; }
        public int Quantity { get; set; }

        [Required]
        [ForeignKey("Basket")]
        public int Basket_id { get; set; }
        public Basket Basket { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int Product_id { get; set; }
        public Product Product { get; set; }
    }
}
