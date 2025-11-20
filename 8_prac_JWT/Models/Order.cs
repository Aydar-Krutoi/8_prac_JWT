using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _8_prac_JWT.Models
{
    public class Order
    {
        [Key]
        public int Order_id { get; set; }
        public DateOnly Order_date { get; set; }
        public double Total_amount { get; set; }

        [Required]
        [ForeignKey("Status")]
        public int Status_id { get; set; }
        public Status Status { get; set; }

        [Required]
        [ForeignKey("User")]
        public int User_id { get; set; }
        public User User { get; set; }

        [Required]
        [ForeignKey("Delivery")]
        public int Delivery_id { get; set; }
        public Delivery Delivery { get; set; }

        [Required]
        [ForeignKey("Payment")]
        public int Payment_id { get; set; }
        public Payment Payment { get; set; }

    }
}
