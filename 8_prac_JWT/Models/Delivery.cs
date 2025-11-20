using System.ComponentModel.DataAnnotations;

namespace _8_prac_JWT.Models
{
    public class Delivery
    {
        [Key]
        public int Delivery_id { get; set; }
        public string Delivery_name { get; set; }
    }
}

