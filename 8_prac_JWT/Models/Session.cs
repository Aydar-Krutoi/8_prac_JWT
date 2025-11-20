using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _8_prac_JWT.Models
{
    public class Session
    {
        [Key]
        public int Session_id { get; set; }
        public string Token { get; set; }

        [Required]
        [ForeignKey("User")]
        public int User_id { get; set; }
        public User User { get; set; }
    }
}
