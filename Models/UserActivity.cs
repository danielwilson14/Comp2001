using System.ComponentModel.DataAnnotations;

namespace Comp2001.Models
{
    public class UserActivity
    {
        [Key]
        public int UserActivityId { get; set; }

        [Required]
        public int UserId { get; set; } // Foreign Key referencing User

        [Required]
        [StringLength(200)]
        public string Activity { get; set; }

    }
}
