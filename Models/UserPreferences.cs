using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comp2001.Models
{
    [Table("UserPreferences", Schema = "CW2")]

    public class UserPreferences
    {
        [Key]
        public int UserId { get; set; }

        public string Units { get; set; }
        public string ActivityTimePreference { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string MarketingLanguage { get; set; }

    }
}
