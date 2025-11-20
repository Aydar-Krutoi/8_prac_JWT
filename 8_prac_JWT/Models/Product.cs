using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _8_prac_JWT.Models
{
    public class Product
    {
        [Key]
        public int Product_id { get; set; }
        public string Product_name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateOnly Created_at { get; set; }
        public bool Is_active { get; set; }
        public int Stock { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int Category_id { get; set; }
        public Category Category { get; set; }
    }
}
