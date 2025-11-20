using System.ComponentModel.DataAnnotations;

namespace _8_prac_JWT.Models
{
    public class Status
    {
        [Key]
        public int Status_id { get; set; }
        public string Status_name { get; set; }
    }
}
