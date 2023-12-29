using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comp2001.Models
{
    [Table("Locations", Schema = "CW2")]

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
