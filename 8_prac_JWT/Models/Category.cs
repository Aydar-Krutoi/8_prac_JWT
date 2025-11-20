using System.ComponentModel.DataAnnotations;

namespace _8_prac_JWT.Models
{
    public class Category
    {
        [Key]
        public int Category_id { get; set; }
        public string Category_name { get; set; }
    }
}
