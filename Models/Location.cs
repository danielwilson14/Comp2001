using System.ComponentModel.DataAnnotations;

namespace Comp2001.Models
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }

        [Required]
        [StringLength(200)]
        public string City { get; set; }

        [Required]
        [StringLength(200)]
        public string Country { get; set; }

    }
}
