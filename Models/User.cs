using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comp2001.Models
{
    [Table("Users", Schema = "CW1")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string AboutMe { get; set; }
        public int LocationID { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public string Password { get; set; }
        public bool Archived { get; set; }
        public bool Admin { get; set; }

    }
}

