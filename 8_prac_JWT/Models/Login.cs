using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _8_prac_JWT.Models
{
    public class Login
    {
        [Key]
        public int Login_id { get; set; }
        public string Login_name { get; set; }
        public string Password { get; set; }

        [Required]
        [ForeignKey("User")]
        public int User_id { get; set; }
        public User User { get; set; }
    }
}
