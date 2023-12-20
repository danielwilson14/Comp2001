using System.ComponentModel.DataAnnotations;

namespace Comp2001.Models
{
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
        public string LocationID { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public string Password { get; set; }
        public string Archived { get; set; }
        public string Admin { get; set; }

    }
}

