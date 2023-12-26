using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comp2001.Models
{
    [Table("UserActivities", Schema = "CW1")]

    public class UserActivity
    {
        [Key]
        public int UserActivityId { get; set; }

        [Required]
        public int UserId { get; set; } // Foreign Key referencing User

        [Required]
        [StringLength(200)]
        public string ActivityName { get; set; }

    }
}
