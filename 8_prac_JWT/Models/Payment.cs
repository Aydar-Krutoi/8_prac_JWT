using System.ComponentModel.DataAnnotations;

namespace _8_prac_JWT.Models
{
    public class Payment
    {
        [Key]
        public int Payment_id { get; set; }
        public string Payment_name { get; set; }
    }
}
