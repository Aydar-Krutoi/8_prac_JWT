using System.ComponentModel.DataAnnotations;

namespace _8_prac_JWT.Models
{
    public class Role
    {
        [Key]
        public int Role_id { get; set; }
        public string Role_name { get; set; }
    }
}
