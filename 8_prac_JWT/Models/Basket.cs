using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _8_prac_JWT.Models
{
    public class Basket
    {
        [Key]
        public int Basket_id { get; set; }
        public double Result_price { get; set; }

        [Required]
        [ForeignKey("User")]
        public int User_id { get; set; }
        public User User { get; set; }

        [ForeignKey("Order")]
        public int? Order_id { get; set; }
        public Order Order { get; set; }
    }
}
