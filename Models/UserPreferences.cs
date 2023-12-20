using System.ComponentModel.DataAnnotations;

namespace Comp2001.Models
{
    public class UserPreferences
    {
        [Key]
        public int UserId { get; set; }

        public string Units { get; set; }
        public string ActivityTimePreference { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public string MarketingLanguage { get; set; }

    }
}
