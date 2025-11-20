using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _8_prac_JWT.Models
{
    public class LogUserAction
    {
        [Key]
        public int Log_user_action_id { get; set; }
        public DateTime Created_at { get; set; }

        [Required]
        [ForeignKey("User")]    
        public int User_id { get; set; }
        public User User { get; set; }

        [Required]
        [ForeignKey("Action")]
        public int Action_id { get; set; }
        public Action_T Action { get; set; }
    }
}
