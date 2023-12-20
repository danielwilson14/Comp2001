namespace Comp2001.DTOs
{
    public class UserPreferencesReadDTO
    {
        public int UserId { get; set; }
        public string Units { get; set; }
        public string ActivityTimePreference { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public string MarketingLanguage { get; set; }
    }

    public class UserPreferencesCreateDTO
    {
        public string Units { get; set; }
        public string ActivityTimePreference { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public string MarketingLanguage { get; set; }
    }

}
