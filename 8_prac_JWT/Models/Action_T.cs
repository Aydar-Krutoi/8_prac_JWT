using System.ComponentModel.DataAnnotations;

namespace _8_prac_JWT.Models
{
    public class Action_T
    {
        [Key]
        public int Action_id { get; set; }
        public string Action_name { get; set; }
        public string Action_description { get; set; }
    }
}
