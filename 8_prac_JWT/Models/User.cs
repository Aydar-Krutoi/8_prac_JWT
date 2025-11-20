using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _8_prac_JWT.Models
{
    public class User
    {
        [Key]
        public int User_id { get; set; }
        public string User_fullname { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly CreatedAt { get; set; }
        public DateOnly UpdatedAt { get; set; }

        [Required]
        [ForeignKey("Role")]
        public int Role_id { get; set; }
        public Role Role { get; set; }
    }
}
